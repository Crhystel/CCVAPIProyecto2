using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : Controller
    {
        private readonly IProfesor _profesor;
        private readonly IMapper _mapper;
        public ProfesoresController(IProfesor profesor, IMapper mapper)
        {
            _profesor = profesor;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Profesor>))]
        public IActionResult GetProfesores()
        {
            var profesores = _mapper.Map<List<ProfesorDto>>(_profesor.GetProfesores());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(profesores);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearProfesor([FromBody] ProfesorDto profesorCreate)
        {
            if (profesorCreate == null)
            {
                ModelState.AddModelError("", "Todos los campos deben estar completos");
                return StatusCode(400, ModelState);
            }
            var profesorUsuarioExistente = _profesor.GetProfesores()
                .FirstOrDefault(c => c.NombreUsuario == profesorCreate.NombreUsuario);
            if (profesorUsuarioExistente != null)
            {
                ModelState.AddModelError("", "El nombre de usuario que ingresó ya existe");
                return StatusCode(422, ModelState);
            }
            var profesores = _profesor.GetProfesores()
                .Where(c => c.Nombre == profesorCreate.Nombre).FirstOrDefault();
            if (profesores != null)
            {
                ModelState.AddModelError("", "Profesor ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Los datos proporcionados no son válidos");
                return BadRequest(ModelState);
            }
            var profesorMap = _mapper.Map<Profesor>(profesorCreate);
            profesorMap.Rol = RolEnum.Profesor;
            if (!_profesor.CreateProfesor(profesorMap))
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
        public IActionResult UpdateProfesor( [FromBody] ProfesorDto profesorUpdate)
        {
            if (profesorUpdate == null)
            {
                ModelState.AddModelError("", "No se encontró el profesor");
                return BadRequest(ModelState);
            }
            var profesorOriginal = _profesor.GetProfesor(profesorUpdate.Id);
            if (profesorOriginal == null)
            {
                return NotFound();
            }
            var duplicadoNombre = _profesor.GetProfesores()
                .Any(e => e.Nombre == profesorUpdate.Nombre && e.Id != profesorUpdate.Id);
            if (duplicadoNombre)
            {
                ModelState.AddModelError("", "El nombre ya está en uso por otro profesor.");
                return StatusCode(422, ModelState);
            }
            var duplicadoNombreUsuario = _profesor.GetProfesores()
                .Any(c => c.NombreUsuario == profesorUpdate.NombreUsuario && c.Id != profesorUpdate.Id);
            if (duplicadoNombreUsuario)
            {
                ModelState.AddModelError("", "El nombre de usuario ya está en uso por otro profesor.");
                return StatusCode(422, ModelState);
            }
            _mapper.Map(profesorUpdate, profesorOriginal);
            if (!_profesor.UpdateProfesor(profesorOriginal))
            {
                ModelState.AddModelError("", "Algo salió mal");
                return StatusCode(500, ModelState);
            }
            
            if (!_profesor.ProfesorExiste(profesorUpdate.Id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfesor(int profesorId)
        {
            if (!_profesor.ProfesorExiste(profesorId))
            {
                return NotFound();
            }
            var profesorDelete = _profesor.GetProfesor(profesorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_profesor.DeleteProfesor(profesorDelete))
            {
                ModelState.AddModelError("", "algo salio mal");
            }
            return NoContent();
        }


    }
}
