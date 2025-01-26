using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseEstudiantesController : Controller
    {
        private readonly IClaseEstudiante _claseEstudiante;
        private readonly IMapper _mapper;
        public ClaseEstudiantesController(IClaseEstudiante claseEstudiante, IMapper mapper)
        {
            _claseEstudiante = claseEstudiante;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClaseEstudiante>))]
        public IActionResult GetClaseEstudiantes()
        {
            var claseEstudiantes = _mapper.Map<List<ClaseEstudianteDto>>(_claseEstudiante.GetClaseEstudiantes()
                .Select(c => new ClaseEstudianteDto
                {
                    Id = c.Id,
                    ClaseId = c.ClaseId,
                    ClaseNombre = c.Clase.Nombre,
                    EstudianteId = c.EstudianteId,
                    EstudianteNombre = c.Estudiante.Nombre
                }).ToList());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(claseEstudiantes);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearClaseEstudiante(  [FromBody] ClaseEstudianteDto claseEstudianteCreate)
        {
            if (claseEstudianteCreate == null)
            {
                ModelState.AddModelError("", "Todos los campos deben estar completos");
                return StatusCode(400, ModelState);
            }
            var claseEstudiantes = _claseEstudiante.GetClaseEstudiantes()
                .Where(c => c.ClaseId == claseEstudianteCreate.ClaseId && c.EstudianteId == claseEstudianteCreate.EstudianteId).FirstOrDefault();
            if (claseEstudiantes != null)
            {
                ModelState.AddModelError("", "Ya unío este estudiante a esta clase");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseEstudianteMap=_mapper.Map<ClaseEstudiante>(claseEstudianteCreate);
            if (!_claseEstudiante.CreateClaseEstudiante(claseEstudianteMap))
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
        public IActionResult UpdateClaseEstudiante([FromBody] ClaseEstudianteDto claseEstudianteUpdate)
        {
            if (claseEstudianteUpdate == null)
            {
                ModelState.AddModelError("", "Todos los campos deben estar llenos");
                return BadRequest(ModelState);
            }
            if (!_claseEstudiante.ClaseEstudianteExiste(claseEstudianteUpdate.Id))
            {
                ModelState.AddModelError("", "ClaseEstudiante no existe");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseEstudianteMap = _mapper.Map<ClaseEstudiante>(claseEstudianteUpdate);
            if (!_claseEstudiante.UpdateClaseEstudiante(claseEstudianteMap))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro ");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteClaseEstudiante([FromQuery] int ceId)
        {
            if(!_claseEstudiante.ClaseEstudianteExiste(ceId))
            {
                ModelState.AddModelError("", "ClaseEstudiante no existe");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseEstudiante = _claseEstudiante.GetClaseEstudiante(ceId);
            if(!_claseEstudiante.DeleteClaseEstudiante(claseEstudiante))
            {
                ModelState.AddModelError("", "Algo salio mal");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }

    }
}
