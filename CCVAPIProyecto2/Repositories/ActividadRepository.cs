﻿using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Repositories
{
    public class ActividadRepository : IActividad
    {
        public readonly DataContext _context;
        public ActividadRepository(DataContext context)
        {
            _context = context;
        }
        public bool ActividadExiste(int actividadId)
        {
            return _context.Actividades.Any(c => c.Id == actividadId);
        }

        public bool CreateActividad(int actividadId, int claseId, Actividad actividad)
        {
            var claseActividad = _context.Clases.Where(c => c.Id == claseId).FirstOrDefault();
            var claseActividadNuevo = new ClaseActividad()
            {
                ClaseId = claseId,
                Actividad = actividad,
            };
            _context.Add(claseActividadNuevo);
            _context.Add(actividad);
            return Save();
        }

        public bool DeleteActividad(Actividad actividad)
        {
            _context.Remove(actividad);
            return Save();
        }

        public Actividad GetActividad(int actividadId)
        {
            return _context.Actividades.Where(c => c.Id == actividadId).FirstOrDefault();
        }

        public ICollection<Actividad> GetActividades()
        {
            return _context.Actividades.OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateActividad(int actividadId, int claseId, Actividad actividad)
        {
            _context.Update(actividad);
            return Save();
        }
    }
}
