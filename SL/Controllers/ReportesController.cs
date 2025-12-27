using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SL.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/reportes")]

    public class ReportesController : ControllerBase
    {
        private readonly BL.Reportes _reportes;

        public ReportesController(BL.Reportes reportes)
        {
            _reportes = reportes;
        }
        [HttpGet("genero")]
        public IActionResult Genero()
        {
            var result = _reportes.DistribucionPorGenero();
            return Ok(result.Objects ?? new List<object>());
        }

        [HttpGet("edad")]
        public IActionResult Edad()
        {
            var result = _reportes.DistribucionPorEdad();
            return Ok(result.Objects ?? new List<object>());
        }

        [HttpGet("ciudad")]
        public IActionResult Ciudad()
        {
            var result = _reportes.DistribucionPorCiudad();
            return Ok(result.Objects ?? new List<object>());
        }

        [HttpGet("nivel-educativo")]
        public IActionResult NivelEducativo()
        {
            var result = _reportes.DistribucionNivelEducativo();
            return Ok(result.Objects ?? new List<object>());
        }

        [HttpGet("experiencia-pago")]
        public IActionResult ExperienciaPago()
        {
            var result = _reportes.ExperienciaVsNivelPago();
            return Ok(result.Objects ?? new List<object>());
        }

    }
}
