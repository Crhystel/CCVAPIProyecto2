using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseActividadesController : Controller
    {
        private readonly IClaseActividad _claseActividad;
        private readonly IMapper _mapper;
        public ClaseActividadesController(IClaseActividad claseActividad, IMapper mapper)
        {
            _claseActividad = claseActividad;
            _mapper = mapper;
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
            var claseActividades=_claseActividad.GetClaseActividades()
                .Where(c => c.ClaseId == claseActividadCreate.ClaseId && c.ActividadId == claseActividadCreate.ActividadId).FirstOrDefault();
            if(claseActividades != null)
            {
                ModelState.AddModelError("", "ClaseActividad ya existe");
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
            return Ok("gucci");
        }
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClaseActividad(int caId,[FromBody] ClaseActividadDto claseActividadUpdate)
        {
            if (claseActividadUpdate == null)
                return BadRequest(ModelState);
            if (!_claseActividad.ClaseActividadExiste(caId))
            {
                ModelState.AddModelError("", "ClaseActividad no existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseActividadMap = _mapper.Map<ClaseActividad>(claseActividadUpdate);
            if (!_claseActividad.UpdateClaseActividad(caId,claseActividadMap))
            { 
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro {claseActividadUpdate}");
                return StatusCode(500, ModelState);
            }
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
    }
}
