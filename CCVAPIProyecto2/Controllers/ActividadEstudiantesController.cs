using AutoMapper;
using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadEstudiantesController : Controller
    {
        private readonly IActividadEstudiante _actividadEstudiante;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ActividadEstudiantesController(IActividadEstudiante actividadEstudiante, IMapper mapper, DataContext context)
        {
            _actividadEstudiante = actividadEstudiante;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ActividadEstudiante>))]
        public IActionResult GetActividadEstudiantes()
        {
            var actividadEstudiantes = _mapper.Map<List<ActividadEstudianteDto>>(_actividadEstudiante.GetActividadEstudiantes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(actividadEstudiantes);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearActividadEstudiante([FromQuery] int actividadId, [FromQuery] int estudianteId, [FromBody] ActividadEstudianteDto actividadEstudianteCreate)
        {
            if (actividadEstudianteCreate == null)
                return BadRequest(ModelState);
            var actividadEstudiantes = _actividadEstudiante.GetActividadEstudiantes()
                .Where(c => c.ActividadId == actividadId && c.EstudianteId == estudianteId).FirstOrDefault();
            if (actividadEstudiantes != null)
            {
                ModelState.AddModelError("", "ActividadEstudiante ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_actividadEstudiante.CreateActividadEstudiante(actividadId, estudianteId))
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
        public IActionResult UpdateActividadEstudiante([FromQuery] int aeId,/*[FromQuery] int actividadId, [FromQuery] int estudianteId,*/ [FromBody] ActividadEstudianteDto actividadEstudianteUpdate)
        {
            if (actividadEstudianteUpdate == null)
                return BadRequest(ModelState);
            if (!_actividadEstudiante.ActividadEstudianteExiste(aeId))
            {
                ModelState.AddModelError("", "ActividadEstudiante no existe");
                return StatusCode(404, ModelState);
            }
            var actividadEstudianteMap = _mapper.Map<ActividadEstudiante>(actividadEstudianteUpdate);
            if (!_actividadEstudiante.UpdateActividadEstudiante(aeId,/*actividadId, estudianteId,*/ actividadEstudianteMap))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro ");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteActividadEstudiante([FromQuery] int aeId)
        {
            var actividadEstudiante = _actividadEstudiante.GetActividadEstudiante(aeId);
            if (actividadEstudiante == null)
            {
                ModelState.AddModelError("", "ActividadEstudiante no existe");
                return StatusCode(404, ModelState);
            }
            if (!_actividadEstudiante.DeleteActividadEstudiante(actividadEstudiante))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando el registro ");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }

        [HttpGet("actividades-por-estudiante")]
        public IActionResult GetActividadesPorEstudiante([FromQuery] int estudianteId)
        {
            var actividades = _context.ActividadEstudiantes
                .Where(ae => ae.EstudianteId == estudianteId)
                .Include(ae => ae.Actividad)
                .Select(ae => new {
                    ae.Actividad.Id,
                    ae.Actividad.Titulo,
                    ae.Actividad.Descripcion,
                    ae.Actividad.FechaEntrega
                })
                .ToList();

            return Ok(actividades);
        }

    }
}
