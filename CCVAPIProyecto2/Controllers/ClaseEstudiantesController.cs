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
            var claseEstudiantes = _mapper.Map<List<ClaseEstudianteDto>>(_claseEstudiante.GetClaseEstudiantes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(claseEstudiantes);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearClaseEstudiante([FromQuery] int claseId, [FromQuery] int estudianteId, [FromBody] ClaseEstudianteDto claseEstudianteCreate)
        {
            if (claseEstudianteCreate == null)
                return BadRequest(ModelState);
            var claseEstudiantes = _claseEstudiante.GetClaseEstudiantes()
                .Where(c => c.ClaseId == claseId && c.EstudianteId == estudianteId).FirstOrDefault();
            if (claseEstudiantes != null)
            {
                ModelState.AddModelError("", "ClaseEstudiante ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_claseEstudiante.CreateClaseEstudiante(claseId, estudianteId/*, claseEstudianteCreate*/))
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
        public IActionResult UpdateClaseEstudiante([FromQuery] int claseId, [FromQuery] int estudianteId, [FromBody] ClaseEstudianteDto claseEstudianteUpdate)
        {
            if (claseEstudianteUpdate == null)
                return BadRequest(ModelState);
            if (!_claseEstudiante.ClaseEstudianteExiste(claseId))
            {
                ModelState.AddModelError("", "ClaseEstudiante no existe");
                return StatusCode(404, ModelState);
            }
            if (!_claseEstudiante.UpdateClaseEstudiante(claseId, _mapper.Map<ClaseEstudiante>(claseEstudianteUpdate)))
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
