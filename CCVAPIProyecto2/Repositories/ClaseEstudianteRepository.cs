using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.EntityFrameworkCore;

namespace CCVAPIProyecto2.Repositories
{
    public class ClaseEstudianteRepository : IClaseEstudiante
    {
        private readonly DataContext _context;
        public ClaseEstudianteRepository(DataContext context)
        {
            _context = context;
        }
        public bool ClaseEstudianteExiste(int ceId)
        {
            return _context.ClaseEstudiantes.Any(c => c.Id == ceId);
        }

        public bool CreateClaseEstudiante(ClaseEstudiante claseEstudiante)
        {
            var clase = _context.Clases.FirstOrDefault(c => c.Id == claseEstudiante.ClaseId);
            var estudiante = _context.Estudiantes.FirstOrDefault(c => c.Id == claseEstudiante.EstudianteId);
            var claseEstudianteNuevo = new ClaseEstudiante
            {
                ClaseId = claseEstudiante.ClaseId,
                EstudianteId = claseEstudiante.EstudianteId,
            };
            _context.Add(claseEstudianteNuevo);
            return Save();
        }

        public bool DeleteClaseEstudiante(ClaseEstudiante claseEstudiante)
        {
            _context.Remove(claseEstudiante);
            return Save();
        }

        public ClaseEstudiante GetClaseEstudiante(int ceId)
        {
            return _context.ClaseEstudiantes.Where(c => c.Id == ceId).FirstOrDefault();
        }

        public ICollection<ClaseEstudiante> GetClaseEstudiantes()
        {
            return _context.ClaseEstudiantes.Include(c => c.Clase).Include(c => c.Estudiante).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClaseEstudiante(int ceId, ClaseEstudiante claseEstudiante)
        {
            _context.Update(claseEstudiante);
            return Save();
        }
    }
}
