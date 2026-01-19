using Microsoft.AspNetCore.Mvc;
using Asuncion.Backend.Traceability.Services;
using Asuncion.Backend.Traceability.Dtos;
namespace Asuncion.Backend.Traceability.Controllers
{
    [ApiController]
    [Route("api/traceability")]
    public class TraceabilityController : ControllerBase
    {
        private readonly TraceabilityService _svc;
        public TraceabilityController(TraceabilityService svc) => _svc = svc;

        [HttpPost("acta")]
        public async Task<IActionResult> Acta([FromBody] TraceabilityRequestDto req)
            => Ok(await _svc.GetTrace(req));
    }
}
