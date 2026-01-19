using Asuncion.Backend.Traceability.Dtos;
using Asuncion.Backend.Traceability.Repositories;

namespace Asuncion.Backend.Traceability.Services
{
    public class CatalogoService
    {
        private readonly CatalogoRepository _repo;
        public CatalogoService(CatalogoRepository repo) => _repo = repo;

        public Task<List<OptionDto>> Provincias() => _repo.GetProvincias();
        public Task<List<OptionDto>> Cantones(int provinciaId) => _repo.GetCantones(provinciaId);
        public Task<List<OptionDto>> Parroquias(int cantonId) => _repo.GetParroquias(cantonId);
        public Task<List<OptionDto>> Zonas(int parroquiaId) => _repo.GetZonas(parroquiaId);
        public Task<List<OptionDto>> Juntas(int parroquiaId, int zonaId) => _repo.GetJuntas(parroquiaId, zonaId);
        public Task<List<OptionDto>> Dignidades(int juntaId) => _repo.GetDignidadesByJunta(juntaId);

    }
}
