﻿using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : Controller
    {
        private readonly IActividad _actividad;
        private readonly IMapper _mapper;
        private readonly IActividad _actividadRepository;
        private readonly IClase _clase;
        public ActividadesController(IActividad actividad, IMapper mapper, IActividad actividadRepository, IClase clase)
        {
            _actividad = actividad;
            _mapper = mapper;
            _actividadRepository = actividadRepository;
            _clase = clase;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Actividad>))]
        public IActionResult GetActividades()
        {
            var actividades = _mapper.Map<List<ActividadDto>>(_actividad.GetActividades());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(actividades);
        }

        [HttpGet("{PorId}")]
        [ProducesResponseType(200, Type = typeof(Actividad))]
        [ProducesResponseType(400)]
        public IActionResult GetActividad(int aId)
        {
            if (!_actividad.ActividadExiste(aId))
                return NotFound();
            var actividad = _mapper.Map<ActividadDto>(_actividad.GetActividad(aId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(actividad);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CrearActividad([FromQuery] int claseId, [FromBody] ActividadDto actividadCreate)
        {
            if (actividadCreate == null)
                return BadRequest(ModelState);

            if (!_clase.ClaseExiste(claseId))
                return NotFound("La clase especificada no existe.");


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actividadMap = _mapper.Map<Actividad>(actividadCreate);
            if (!_actividad.CreateActividad(claseId, actividadMap)) // Nota: Elimina el parámetro actividadId
            {
                ModelState.AddModelError("", "Algo salió mal al crear la actividad.");
                return StatusCode(500, ModelState);
            }

            return Ok("Actividad creada con éxito.");
        }



        [HttpPut/*("{actividadId}")*/]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateActividad(/*[FromQuery] int claseId,*/  int actividadId, [FromBody] ActividadDto actividadUpdate)
        {
            if (actividadUpdate == null)
                return BadRequest(ModelState);
            if (actividadId != actividadUpdate.Id)
                return BadRequest(ModelState);
            if (!_actividad.ActividadExiste(actividadId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var actividadesMap = _mapper.Map<Actividad>(actividadUpdate);
            if (!_actividad.UpdateActividad(/*claseId, */actividadId, actividadesMap))
            {
                ModelState.AddModelError("", "Algo saliomal");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{actividadId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteActividad(int actividadId)
        {
            if (!_actividad.ActividadExiste(actividadId))
            {
                return NotFound();
            }
            var actividadDelete = _actividad.GetActividad(actividadId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_actividad.DeleteActividad(actividadDelete))
            {
                ModelState.AddModelError("", "algo salio mal");
            }
            return NoContent();
        }

        [HttpGet("PorClase/{claseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ActividadDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetActividadesPorClase(int claseId)
        {
            var actividades = _mapper.Map<List<ActividadDto>>(_actividad.GetActividadesPorClase(claseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(actividades);
        }



    }
}
