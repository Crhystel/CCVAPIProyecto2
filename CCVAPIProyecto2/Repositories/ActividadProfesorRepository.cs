using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Repositories
{
    public class ActividadProfesorRepository : IActividadProfesor
    {
        private readonly DataContext _context;
        public ActividadProfesorRepository(DataContext context)
        {
            _context = context;
        }

        public bool ActividadProfesorExiste(int apId)
        {
            return _context.ActividadProfesores.Any(a => a.Id == apId);
        }

        public bool CreateActividadProfesor(int actividadId, int profesorId)
        {
            var actividad = _context.Actividades.FirstOrDefault(a => a.Id == actividadId);
            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == profesorId);
            var actividadProfesor = new ActividadProfesor
            {
                ActividadId = actividadId,
                ProfesorId = profesorId,
            };
            _context.Add(actividadProfesor);
            return Save();
        }

        public bool DeleteActividadProfesor(ActividadProfesor actividadProfesor)
        {
            _context.Remove(actividadProfesor);
            return Save();
        }

        public ActividadProfesor GetActividadProfesor(int apId)
        {
            return _context.ActividadProfesores.Where(a => a.Id == apId).FirstOrDefault();
        }

        public ICollection<ActividadProfesor> GetActividadProfesores()
        {
            return _context.ActividadProfesores.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateActividadProfesor(int apId, ActividadProfesor actividadProfesor)
        {
            _context.Update(actividadProfesor);
            return Save();
        }
    }
}
