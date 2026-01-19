using Microsoft.AspNetCore.Mvc;
using Asuncion.Backend.Traceability.Services;

namespace Asuncion.Backend.Traceability.Controllers
{
    [ApiController]
    [Route("api/catalogo")]
    public class CatalogoController : ControllerBase
    {
        private readonly CatalogoService _svc;
        public CatalogoController(CatalogoService svc) => _svc = svc;

        [HttpGet("provincias")]
        public async Task<IActionResult> Provincias() => Ok(await _svc.Provincias());

        [HttpGet("cantones")]
        public async Task<IActionResult> Cantones([FromQuery] int provinciaId) => Ok(await _svc.Cantones(provinciaId));

        [HttpGet("parroquias")]
        public async Task<IActionResult> Parroquias([FromQuery] int cantonId) => Ok(await _svc.Parroquias(cantonId));

        [HttpGet("zonas")]
        public async Task<IActionResult> Zonas([FromQuery] int parroquiaId) => Ok(await _svc.Zonas(parroquiaId));

        // OJO: esto es lo real por tu DB
        [HttpGet("juntas")]
        public async Task<IActionResult> Juntas([FromQuery] int parroquiaId, [FromQuery] int zonaId)
            => Ok(await _svc.Juntas(parroquiaId, zonaId));

        [HttpGet("dignidades")]
        public async Task<IActionResult> Dignidades([FromQuery] int juntaId) => Ok(await _svc.Dignidades(juntaId));
    }
}
