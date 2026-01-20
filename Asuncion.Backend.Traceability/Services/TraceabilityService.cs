using Asuncion.Backend.Traceability.Dtos;
using Asuncion.Backend.Traceability.Repositories;

namespace Asuncion.Backend.Traceability.Services
{
    public class TraceabilityService
    {
        private readonly TraceabilityRepository _repo;
        private readonly IConfiguration _cfg;

        public TraceabilityService(TraceabilityRepository repo, IConfiguration cfg)
        {
            _repo = repo;
            _cfg = cfg;
        }

        public async Task<ActaTraceDto> GetTrace(TraceabilityRequestDto req)
        {
            var core = await _repo.GetActaCore(req.juntaId, req.dignidadId);

            // ✅ lista de imágenes desde DB (cada una con PathIpfs)
            var imagenes = await _repo.GetImagenesActa(core.actaId);

            var resultados = await _repo.GetResultadosFinales(core.actaId);

            var steps = new TraceabilityStepsDto
            {
                escaneo = BuildStep(core.txIcr, core.fIcr, metadata: null, result: null),
                digitacion = BuildStep(core.txDig, core.fDig, metadata: null, result: null),
                control = BuildStep(core.txCtrl, core.fCtrl, metadata: null, result: resultados?.estadoFinal)
            };

            return new ActaTraceDto
            {
                actaId = core.actaId,
                codigo = core.codigo,
                scope = core.scope,
                imagenes = imagenes,   // ✅ aquí
                steps = steps,
                resultados = resultados,
                lastUpdated = DateTime.UtcNow.ToString("O")
            };
        }


        private static StepDto BuildStep(string? txHash, DateTime? timestamp, object? metadata, string? result)
        {
            return new StepDto
            {
                txHash = txHash ?? string.Empty,
                timestamp = timestamp?.ToUniversalTime().ToString("O"),
                block = null,       // si luego consultas block height, lo pones aquí
                metadata = metadata, // si guardas metadataJson en DB, aquí lo asignas
                result = result      // opcional, útil en control
            };
        }

        private static string CleanCid(string cidOrUrl)
        {
            // permite que guardes "ipfs://CID" o "/ipfs/CID" o "CID"
            return cidOrUrl
                .Replace("ipfs://", "")
                .Replace("/ipfs/", "")
                .Trim();
        }
    }
}
