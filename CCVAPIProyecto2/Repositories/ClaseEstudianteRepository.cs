﻿using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;

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

        public bool CreateClaseEstudiante(int claseId, int estudianteId)
        {
            var clase = _context.Clases.FirstOrDefault(c => c.Id == claseId);
            var estudiante = _context.Estudiantes.FirstOrDefault(c => c.Id == estudianteId);
            var claseEstudiante = new ClaseEstudiante
            {
                ClaseId = claseId,
                EstudianteId = estudianteId,
            };
            _context.Add(claseEstudiante);
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
            return _context.ClaseEstudiantes.OrderBy(c => c.Id).ToList();
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
