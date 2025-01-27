using CCVAPIProyecto2.Data;
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

        public bool CreateActividad(int claseID, Actividad actividad)
        {
            
            var clase = _context.Clases.FirstOrDefault(c => c.Id == claseID);
            if (clase == null)
            {
                throw new ArgumentException("La clase especificada no existe.");
            }

            
            var claseActividad = new ClaseActividad
            {
                ClaseId = claseID,
                Actividad = actividad
            };

            
            _context.ClaseActividades.Add(claseActividad);

            
            _context.Actividades.Add(actividad);

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

        public bool UpdateActividad(int actividadId,  Actividad actividad)
        {
            _context.Update(actividad);
            return Save();
        }
        public ICollection<Actividad> GetActividadesPorClase(int claseId)
        {
            return _context.ClaseActividades
                .Where(ca => ca.ClaseId == claseId)
                .Select(ca => ca.Actividad)
                .ToList();
        }
    }
}
