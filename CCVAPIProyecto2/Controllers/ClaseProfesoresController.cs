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
            var claseProfesores = _mapper.Map<List<ClaseProfesorDto>>(_claseProfesor.GetClaseProfesores()
                .Select(c => new ClaseProfesorDto
                {
                    Id = c.Id,
                    ClasePId = c.ClasePId,
                    ClaseNombre = c.ClaseP.Nombre,
                    ProfesorId = c.ProfesorId,
                    ProfesorNombre = c.Profesor.Nombre
                }).ToList());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(claseProfesores);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearClaseProfesor([FromBody] ClaseProfesorDto claseProfesorCreate)
        {
            if (claseProfesorCreate == null)
                return BadRequest(ModelState);
            var claseProfesores = _claseProfesor.GetClaseProfesores()
                .Where(c => c.ClasePId == claseProfesorCreate.ClasePId && c.ProfesorId == claseProfesorCreate.ProfesorId).FirstOrDefault();
            if (claseProfesores != null)
            {
                ModelState.AddModelError("", "ClaseProfesor ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseProfesorMap = _mapper.Map<ClaseProfesor>(claseProfesorCreate);
            if (!_claseProfesor.CreateClaseProfesor(claseProfesorMap))
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
        public IActionResult UpdateClaseProfesor(int cpId ,[FromBody] ClaseProfesorDto claseProfesorUpdate)
        {
            if (claseProfesorUpdate == null)
                return BadRequest(ModelState);
            if (!_claseProfesor.ClaseProfesorExiste(cpId))
            {
                ModelState.AddModelError("", "ClaseProfesor no existe");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseProfesor = _mapper.Map<ClaseProfesor>(claseProfesorUpdate);
            if (!_claseProfesor.UpdateClaseProfesor(cpId, claseProfesor))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro");
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
