using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;

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

        public bool CreateClaseProfesor(int claseId, int profesorId)
        {
            var clase = _context.Clases.FirstOrDefault(c => c.Id == claseId);
            var profesor = _context.Profesores.FirstOrDefault(c => c.Id == profesorId);
            var claseProfesor = new ClaseProfesor
            {
                ClasePId = claseId,
                ProfesorId = profesorId,
            };
            _context.Add(claseProfesor);
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
            return _context.ClaseProfesores.OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
             var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClaseProfesor(int cpId, ClaseProfesor claseProfesor)
        {
            _context.Update(claseProfesor);
            return true;
        }
    }
}
