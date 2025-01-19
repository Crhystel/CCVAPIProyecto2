using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadProfesoresController : Controller
    {
        private readonly IActividadProfesor _actividadProfesor;
        private readonly IMapper _mapper;
        public ActividadProfesoresController(IActividadProfesor actividadProfesor, IMapper mapper)
        {
            _actividadProfesor = actividadProfesor;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ActividadProfesor>))]
        public IActionResult GetActividadprofesores()
        {
            var actividadProfesor = _mapper.Map<List<ActividadProfesorDto>>(_actividadProfesor.GetActividadProfesores());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(actividadProfesor);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearActividadProfesor([FromQuery] int actividadId, [FromQuery] int profesorId, [FromBody] ActividadProfesorDto actividadProfesorCreate)
        {
            if (actividadProfesorCreate == null)
                return BadRequest(ModelState);
            var actividadProfesor = _actividadProfesor.GetActividadProfesores()
                .Where(c => c.ActividadId == actividadId && c.ProfesorId == profesorId).FirstOrDefault();
            if (actividadProfesor != null)
            {
                ModelState.AddModelError("", "ActividadProfesor ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_actividadProfesor.CreateActividadProfesor(actividadId, profesorId))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro ");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateActividadProfesor([FromQuery] int apId, [FromBody] ActividadProfesorDto actividadProfesorUpdate)
        {
            if (actividadProfesorUpdate == null)
                return BadRequest(ModelState);
            if (!_actividadProfesor.ActividadProfesorExiste(apId))
            {
                ModelState.AddModelError("", "ActividadProfesor no existe");
                return StatusCode(404, ModelState);
            }
            var actividadProfesor = _mapper.Map<ActividadProfesor>(actividadProfesorUpdate);
            if (!_actividadProfesor.UpdateActividadProfesor(apId, actividadProfesor))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteActividadProfesor([FromQuery] int apId)
        {
            var actividadProfesor = _actividadProfesor.GetActividadProfesor(apId);
            if (actividadProfesor == null)
            {
                ModelState.AddModelError("", "ActividadProfesor no existe");
                return StatusCode(404, ModelState);
            }
            if (!_actividadProfesor.DeleteActividadProfesor(actividadProfesor))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando el registro");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }

    }
}
