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
            var claseActividades = _mapper.Map<List<ClaseActividadDto>>(_claseActividad.GetClaseActividades());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(claseActividades);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearClaseActividad([FromQuery] int claseId, [FromQuery] int actividadId, [FromBody] ClaseActividadDto claseActividadCreate)
        {
            //if (claseActividadCreate == null)
            //    return BadRequest(ModelState);
            if (_claseActividad.ClaseActividadExiste(claseId, actividadId))
            {
                ModelState.AddModelError("", "ClaseActividad ya existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_claseActividad.CreateClaseActividad(claseId, actividadId/*, claseActividadCreate*/))
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
        public IActionResult UpdateClaseActividad([FromQuery] int claseId, [FromQuery] int actividadId, [FromBody] ClaseActividadDto claseActividadUpdate)
        {
            if (claseActividadUpdate == null)
                return BadRequest(ModelState);
            if (!_claseActividad.ClaseActividadExiste(claseId, actividadId))
            {
                ModelState.AddModelError("", "ClaseActividad no existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseActividadMap = _mapper.Map<ClaseActividad>(claseActividadUpdate);
            if (!_claseActividad.UpdateClaseActividad(claseId, actividadId, claseActividadMap))
            { 
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro {claseActividadUpdate}");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }
        [HttpDelete("{caId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClaseActividad(int caId,[FromQuery] int claseId, [FromQuery] int actividadId, [FromBody] ClaseActividadDto claseActividadUpdate)
        {
            if (!_claseActividad.ClaseActividadExiste(claseId, actividadId))
            {
                ModelState.AddModelError("", "ClaseActividad no existe");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var claseActividadDelete=_claseActividad.GetClaseActividad(claseId, actividadId);
            if (!_claseActividad.DeleteClaseActividad(claseActividadDelete))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminando el registro {claseId} {actividadId}");
                return StatusCode(500, ModelState);
            }
            return Ok("gucci");
        }
    }
}
