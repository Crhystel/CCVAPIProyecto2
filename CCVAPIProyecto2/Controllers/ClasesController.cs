using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasesController : Controller
    {
        private readonly IClase _clase;
        private readonly IMapper _mapper;
        public ClasesController(IClase clase, IMapper mapper)
        {
            _clase = clase;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Clase>))]
        public IActionResult GetClases()
        {
            var clases = _mapper.Map<List<ClaseDto>>(_clase.GetClases());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(clases);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearClase([FromBody] ClaseDto claseCreate)
        {
            if (claseCreate == null)
            {
                ModelState.AddModelError("", "Todos los campos deben estar completos");
                return StatusCode(400, ModelState);
            }
            var claseNombreExistente = _clase.GetClases()
                .FirstOrDefault(c => c.Nombre == claseCreate.Nombre);
            if (claseNombreExistente != null)
            {
                ModelState.AddModelError("", "El nombre de la clase ya existe");
                return StatusCode(422, ModelState);
            }
            var clases = _clase.GetClases()
                .Where(c => c.Id == claseCreate.Id).FirstOrDefault();
            if (clases != null)
            {
                ModelState.AddModelError("", "Clase ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Los datos proporcionados no son válidos");
                return BadRequest(ModelState);
            }
            var claseMap = _mapper.Map<Clase>(claseCreate);
            if (!_clase.CreateClase(claseMap))
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
        public IActionResult UpdateClase([FromBody] ClaseDto claseUpdate)
        {
            if (claseUpdate == null)
            {
                ModelState.AddModelError("", "No se encontró la clase");
                return BadRequest(ModelState);
            }

            if (!_clase.ClaseExiste(claseUpdate.Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var claseMap = _mapper.Map<Clase>(claseUpdate);
            if (!_clase.UpdateClase(claseMap)) 
            {
                ModelState.AddModelError("", "Algo salio mal");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClase(int claseId)
        {
            if (!_clase.ClaseExiste(claseId))
            {
                return NotFound();
            }
            var claseDelete = _clase.GetClase(claseId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_clase.DeleteClase(claseDelete))
            {
                ModelState.AddModelError("", "algo salio mal");
            }
            return NoContent();
        }
    }
}
