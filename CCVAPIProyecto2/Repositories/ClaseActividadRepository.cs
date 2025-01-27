using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.EntityFrameworkCore;

namespace CCVAPIProyecto2.Repositories
{
    public class ClaseActividadRepository : IClaseActividad
    {
        private readonly DataContext _context;
        public ClaseActividadRepository(DataContext context)
        {
            _context = context;
        }

        public bool ClaseActividadExiste(int caId)
        {
            return _context.ClaseActividades.Any(c => c.Id==caId);
        }

        public bool CreateClaseActividad(ClaseActividad claseActividad)
        {
            var clase = _context.Clases.FirstOrDefault(c => c.Id == claseActividad.ClaseId);
            var actividad= _context.Actividades.FirstOrDefault(c => c.Id==claseActividad.ActividadId);
            var claseActividads = new ClaseActividad
            {
                ClaseId = claseActividad.ClaseId,
                ActividadId = claseActividad.ActividadId,
            };
            _context.Add(claseActividads);
            return Save();
        }

        public bool DeleteClaseActividad(ClaseActividad claseActividad)
        {
            _context.Remove(claseActividad);
            return Save();
        }

        public ClaseActividad GetClaseActividad(int caId)
        {
            return _context.ClaseActividades.Where(c=>c.Id==caId).FirstOrDefault();
        }

        public ICollection<ClaseActividad> GetClaseActividades()
        {
            return _context.ClaseActividades.Include(c=>c.Clase).Include(c=>c.Actividad).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClaseActividad(ClaseActividad claseActividad)
        {
            var trackedEntity = _context.ClaseEstudiantes.Local.FirstOrDefault(c => c.Id == claseActividad.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
            _context.Attach(claseActividad);
            _context.Entry(claseActividad).State = EntityState.Modified;
            return Save();
        }
        public bool AsignarActividadAEstudiantes(int claseId, int actividadId)
        {
            var clase = _context.Clases.Include(c => c.ClaseEstudiantes).FirstOrDefault(c => c.Id == claseId);
            if (clase == null) return false;

            foreach (var estudiante in clase.ClaseEstudiantes)
            {
                var actividadEstudiante = new ActividadEstudiante
                {
                    EstudianteId = estudiante.EstudianteId,
                    ActividadId = actividadId
                };

                var existente = _context.ActividadEstudiantes
                    .FirstOrDefault(ae => ae.EstudianteId == actividadEstudiante.EstudianteId && ae.ActividadId == actividadEstudiante.ActividadId);

                if (existente == null)
                {
                    _context.ActividadEstudiantes.Add(actividadEstudiante);
                }
            }

            return _context.SaveChanges() > 0;
        }

    }
}
