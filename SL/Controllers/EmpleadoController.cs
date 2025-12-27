using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmpleadoController : ControllerBase
    {
        private readonly BL.Empleado _empleadoBL;

        public EmpleadoController(DL.IvelazquezPruebaKpmgContext context)
        {
            _empleadoBL = new BL.Empleado(context);
        }
        [Authorize(Roles ="RRHH,Gerente")]
        [HttpPost("GetAll")]
        public IActionResult GetAll([FromBody] ML.Empleado empleado)
        {
            ML.Result result = _empleadoBL.GetAll(empleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [Authorize(Roles = "Gerente,RRHH")]

        [HttpGet("GetById/{IdEmpleado}")]
        public IActionResult GetById(int IdEmpleado)
        {
            ML.Result result = _empleadoBL.GetByID(IdEmpleado);

            if (result.Correct)
                return Ok(result);

            return NotFound(result);
        }
        [Authorize(Roles = "RRHH")]

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Empleado empleado)
        {
            ML.Result result = _empleadoBL.Add(empleado);

            if (result.Correct)
                return Ok(result);

            return BadRequest(result);
        }
        [Authorize(Roles = "RRHH")]

        [HttpPut("Update")]
        public IActionResult Update([FromBody] ML.Empleado empleado)
        {
            ML.Result result = _empleadoBL.Update(empleado);

            if (result.Correct)
                return Ok(result);

            return BadRequest(result);
        }
        [Authorize(Roles = "RRHH")]

        [HttpDelete("Delete/{idEmpleado}")]
        public IActionResult Delete(int idEmpleado)
        {
            ML.Result result = _empleadoBL.Delete(idEmpleado);

            if (result.Correct)
                return Ok(result);

            return NotFound(result);
        }
    }

}
