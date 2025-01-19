using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Repositories
{
    public class ClaseActividadRepository : IClaseActividad
    {
        private readonly DataContext _context;
        public ClaseActividadRepository(DataContext context)
        {
            _context = context;
        }

        public bool ClaseActividadExiste(int claseId, int actividadId)
        {
            return _context.ClaseActividades.Any(c => c.ClaseId == claseId && c.ActividadId == actividadId);
        }

        public bool CreateClaseActividad(int claseId, int actividadId/*, ClaseActividad claseActividad*/)
        {
            var clase = _context.Clases.FirstOrDefault(c => c.Id == claseId);
            var actividad= _context.Actividades.FirstOrDefault(c => c.Id==actividadId);
            var claseActividads = new ClaseActividad
            {
                ClaseId = claseId,
                ActividadId = actividadId,
            };
            _context.Add(claseActividads);
            return Save();
        }

        public bool DeleteClaseActividad(ClaseActividad claseActividad)
        {
            _context.Remove(claseActividad);
            return Save();
        }

        public ClaseActividad GetClaseActividad(int claseId, int actividadId)
        {
            return _context.ClaseActividades.Where(c=>c.ClaseId == claseId && c.ActividadId==actividadId).FirstOrDefault();
        }

        public ICollection<ClaseActividad> GetClaseActividades()
        {
            return _context.ClaseActividades.OrderBy(c=>c.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClaseActividad(int claseId, int actividadId, ClaseActividad claseActividad)
        {
            _context.Update(claseActividad);
            return Save();
        }
    }
}
