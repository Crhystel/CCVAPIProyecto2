using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseProfesoresController : Controller
    {
        private readonly IClaseProfesor _claseProfesor;
        private readonly IMapper _mapper;
        public ClaseProfesoresController(IClaseProfesor claseProfesor, IMapper mapper)
        {
            _claseProfesor = claseProfesor;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClaseProfesor>))]
        public IActionResult GetClaseProfesores()
        {
            var claseProfesores = _mapper.Map<List<ClaseProfesorDto>>(_claseProfesor.GetClaseProfesores());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(claseProfesores);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearClaseProfesor([FromQuery] int claseId, [FromQuery] int profesorId, [FromBody] ClaseProfesorDto claseProfesorCreate)
        {
            if (claseProfesorCreate == null)
                return BadRequest(ModelState);
            var claseProfesores = _claseProfesor.GetClaseProfesores()
                .Where(c => c.ClasePId == claseId && c.ProfesorId == profesorId).FirstOrDefault();
            if (claseProfesores != null)
            {
                ModelState.AddModelError("", "ClaseProfesor ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_claseProfesor.CreateClaseProfesor(claseId, profesorId))
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
        public IActionResult UpdateClaseProfesor([FromQuery] int claseId, [FromQuery] int profesorId, [FromBody] ClaseProfesorDto claseProfesorUpdate)
        {
            if (claseProfesorUpdate == null)
                return BadRequest(ModelState);
            if (!_claseProfesor.ClaseProfesorExiste(claseId))
            {
                ModelState.AddModelError("", "ClaseProfesor no existe");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseProfesor = _mapper.Map<ClaseProfesor>(claseProfesorUpdate);
            if (!_claseProfesor.UpdateClaseProfesor(claseId, claseProfesor))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro {claseId}");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci"); 
        }
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteClaseProfesor([FromQuery] int cpId)
        {
            var claseProfesor = _claseProfesor.GetClaseProfesor(cpId);
            if (claseProfesor == null)
            {
                ModelState.AddModelError("", "ClaseProfesor no existe");
                return StatusCode(404, ModelState);
            }
            if (!_claseProfesor.DeleteClaseProfesor(claseProfesor))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando el registro {cpId}");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }
    }
}
