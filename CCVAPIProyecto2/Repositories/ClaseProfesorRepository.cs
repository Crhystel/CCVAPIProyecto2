using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.EntityFrameworkCore;

namespace CCVAPIProyecto2.Repositories
{
    public class ClaseProfesorRepository : IClaseProfesor
    {
        private readonly DataContext _context;
        public ClaseProfesorRepository(DataContext context)
        {
            _context = context;
        }
        public bool ClaseProfesorExiste(int cpId)
        {
            return _context.ClaseProfesores.Any(c => c.Id == cpId);
        }

        public bool CreateClaseProfesor(ClaseProfesor claseProfesor)
        {
            var clase = _context.Clases.FirstOrDefault(c => c.Id == claseProfesor.ClasePId);
            var profesor = _context.Profesores.FirstOrDefault(c => c.Id == claseProfesor.ProfesorId);
            var claseProfesorNuevo = new ClaseProfesor
            {
                ClasePId = claseProfesor.ClasePId,
                ProfesorId = claseProfesor.ProfesorId,
            };
            _context.Add(claseProfesorNuevo);
            return Save();
        }

        public bool DeleteClaseProfesor(ClaseProfesor claseProfesor)
        {
            _context.Remove(claseProfesor);
            return Save();
        }

        public ClaseProfesor GetClaseProfesor(int cpId)
        {
            return _context.ClaseProfesores.Where(c => c.Id == cpId).FirstOrDefault();
        }

        public ICollection<ClaseProfesor> GetClaseProfesores()
        {
            return _context.ClaseProfesores.Include(c => c.ClaseP).Include(c => c.Profesor).ToList();
        }

        public bool Save()
        {
             var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClaseProfesor(ClaseProfesor claseProfesor)
        {
            var trackedEntity = _context.ClaseEstudiantes.Local.FirstOrDefault(c => c.Id == claseProfesor.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
            _context.Attach(claseProfesor);
            _context.Entry(claseProfesor).State = EntityState.Modified;
            return Save();
        }
    }
}
