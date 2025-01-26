using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : Controller
    {
        private readonly IEstudiante _estudiante;
        private readonly IMapper _mapper;
        private readonly IActividadEstudiante _actividadEstudiante;
        //Inyeccion de dependencias
        public EstudiantesController(IEstudiante estudiante,
            IActividadEstudiante actividadEstudiante, IMapper mapper)
        {
            _estudiante = estudiante;
            _mapper = mapper;
            _actividadEstudiante = actividadEstudiante;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Estudiante>))]
        public IActionResult GetEstudiantes()
        {
            var estudiantes = _mapper.Map<List<EstudianteDto>>(_estudiante.GetEstudiantes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(estudiantes);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearEstudiante( [FromBody] EstudianteDto estudianteCreate)
        {
            if (estudianteCreate == null)
                return BadRequest(ModelState);
            var estudiantes = _estudiante.GetEstudiantes()
                .Where(c => c.Nombre == estudianteCreate.Nombre).FirstOrDefault();
            if (estudiantes != null)
            {
                ModelState.AddModelError("", "Estudiante ya existe");
                return StatusCode(422, ModelState);
            }
            var estudianteUsuario = _estudiante.GetEstudiantes()
                .Where(c => c.NombreUsuario == estudianteCreate.NombreUsuario).FirstOrDefault();
            if (estudianteUsuario != null)
            {
                ModelState.AddModelError("", "Nombre de usuario ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var estudianteMap = _mapper.Map<Estudiante>(estudianteCreate);
            estudianteMap.Rol = RolEnum.Estudiante;
            if (!_estudiante.CreateEstudiante(estudianteMap))
            {
                ModelState.AddModelError("", "Algo salio mal");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEstudiante([FromBody] EstudianteDto estudianteUpdate)
        {
            if (estudianteUpdate == null)
            {
                ModelState.AddModelError("", "No se encontró el estudiante");
                return BadRequest(ModelState);
            }

            if (!_estudiante.EstudianteExiste(estudianteUpdate.Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estudianteMap = _mapper.Map<Estudiante>(estudianteUpdate);

            if (!_estudiante.UpdateEstudiante(estudianteMap))
            {
                ModelState.AddModelError("", "Algo salió mal");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEstudiante(int estudianteId)
        {
            if (!_estudiante.EstudianteExiste(estudianteId))
            {
                return NotFound();
            }
            var estudianteDelete = _estudiante.GetEstudiante(estudianteId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_estudiante.DeleteEstudiante(estudianteDelete))
            {
                ModelState.AddModelError("", "algo salio mal");
            }
            return NoContent();
        }
    }
}
