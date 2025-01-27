using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCVAPIProyecto2.Data;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseActividadesController : Controller
    {
        private readonly IClaseActividad _claseActividad;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ClaseActividadesController(IClaseActividad claseActividad, IMapper mapper, DataContext context)
        {
            _claseActividad = claseActividad;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClaseActividad>))]
        public IActionResult GetClaseActividades()
        {
            var claseActividades = _mapper.Map<List<ClaseActividadDto>>(_claseActividad.GetClaseActividades()
                .Select(c => new ClaseActividadDto
                {
                    Id = c.Id,
                    ClaseId = c.ClaseId,
                    ClaseNombre = c.Clase.Nombre,
                    ActividadId = c.ActividadId,
                    ActividadNombre = c.Actividad.Titulo

                }).ToList());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(claseActividades);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearClaseActividad([FromBody] ClaseActividadDto claseActividadCreate)
        {
            if (claseActividadCreate == null)
                return BadRequest(ModelState);

            // Verificar si el profesor enseña esta clase
            var esProfesorDeLaClase = _context.ClaseProfesores
                .Any(cp => cp.ClasePId == claseActividadCreate.ClaseId && cp.ProfesorId == claseActividadCreate.ProfesorId); // Asegúrate de que ClaseActividadDto tenga ProfesorId
            if (!esProfesorDeLaClase)
            {
                return Unauthorized("El profesor no enseña esta clase.");
            }

            // Verificar si la actividad ya está asignada a esta clase
            var claseActividades = _claseActividad.GetClaseActividades()
                .Where(c => c.ClaseId == claseActividadCreate.ClaseId && c.ActividadId == claseActividadCreate.ActividadId).FirstOrDefault();
            if (claseActividades != null)
            {
                ModelState.AddModelError("", "La actividad ya está asignada a esta clase");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var claseActividadMap = _mapper.Map<ClaseActividad>(claseActividadCreate);
            if (!_claseActividad.CreateClaseActividad(claseActividadMap))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro ");
                return StatusCode(500, ModelState);
            }

            return Ok("Actividad asignada correctamente.");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClaseActividad([FromBody] ClaseActividadDto claseActividadUpdate)
        {
            if (claseActividadUpdate == null)
            {
                ModelState.AddModelError("", "Todos los campos deben estar llenos");
                return BadRequest(ModelState);
            }
            var claseActividadOriginal = _claseActividad.GetClaseActividad(claseActividadUpdate.Id);
            if (claseActividadOriginal == null)
            {
                return NotFound();
            }
            if (claseActividadOriginal == null)
            {
                return NotFound();
            }
            var duplicado = _claseActividad.GetClaseActividades()
                .Any(c => c.ClaseId == claseActividadUpdate.ClaseId && c.ActividadId == claseActividadUpdate.ActividadId && c.Id != claseActividadUpdate.Id);
            if (duplicado)
            {
                ModelState.AddModelError("", "La actividad ya está asignada a esta clase");
                return StatusCode(422, ModelState);
            }
            _mapper.Map(claseActividadUpdate, claseActividadOriginal);
            if (!_claseActividad.UpdateClaseActividad(claseActividadOriginal))
            {
                ModelState.AddModelError("", "Algo salió mal");
                return StatusCode(500, ModelState);
            }
            if (!_claseActividad.ClaseActividadExiste(claseActividadUpdate.Id))
            {
                ModelState.AddModelError("", "ClaseActividad no existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok("gucci");
        }
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClaseActividad([FromQuery] int caId)
        {
            if (!_claseActividad.ClaseActividadExiste(caId))
            {
                ModelState.AddModelError("", "ClaseActividad no existe");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseActividadDelete=_claseActividad.GetClaseActividad(caId);
            if (!_claseActividad.DeleteClaseActividad(claseActividadDelete))
            {
                ModelState.AddModelError("", $"Algo salio mal");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }

        [HttpPost("asignar-a-estudiantes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult AsignarActividadAEstudiantes([FromQuery] int claseId, [FromQuery] int actividadId)
        {
            // Verificar si la clase existe
            var clase = _context.Clases.Include(c => c.ClaseEstudiantes).FirstOrDefault(c => c.Id == claseId);
            if (clase == null)
            {
                return NotFound("Clase no encontrada.");
            }

            // Verificar si la actividad existe
            var actividad = _context.Actividades.FirstOrDefault(a => a.Id == actividadId);
            if (actividad == null)
            {
                return NotFound("Actividad no encontrada.");
            }

            // Crear las relaciones ActividadEstudiante para cada estudiante de la clase
            foreach (var estudiante in clase.ClaseEstudiantes)
            {
                var actividadEstudiante = new ActividadEstudiante
                {
                    EstudianteId = estudiante.EstudianteId,
                    ActividadId = actividad.Id
                };

                // Verificar si ya existe la relación
                var existente = _context.ActividadEstudiantes
                    .FirstOrDefault(ae => ae.EstudianteId == actividadEstudiante.EstudianteId && ae.ActividadId == actividadEstudiante.ActividadId);

                if (existente == null)
                {
                    _context.ActividadEstudiantes.Add(actividadEstudiante);
                }
            }

            // Guardar los cambios
            if (_context.SaveChanges() > 0)
            {
                return Ok("Actividad asignada a todos los estudiantes de la clase.");
            }

            return StatusCode(500, "Error asignando actividad.");
        }

    }
}
