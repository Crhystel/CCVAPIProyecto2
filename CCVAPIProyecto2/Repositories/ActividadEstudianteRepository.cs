using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Repositories
{
    public class ActividadEstudianteRepository : IActividadEstudiante
    {
        private readonly DataContext _context;
        public ActividadEstudianteRepository(DataContext context)
        {
            _context = context;
        }

        public bool ActividadEstudianteExiste(int aeId)
        {
            return _context.ActividadEstudiantes.Any(a => a.Id == aeId);
        }

        public bool CreateActividadEstudiante(int actividadId, int estudianteId)
        {
            var actividad = _context.Actividades.FirstOrDefault(a => a.Id == actividadId);
            var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Id == estudianteId);
            var actividadEstudiante = new ActividadEstudiante
            {
                ActividadId = actividadId,
                EstudianteId = estudianteId,
            };
            _context.Add(actividadEstudiante);
            return Save();
        }

        public bool DeleteActividadEstudiante(ActividadEstudiante actividadEstudiante)
        {
            _context.Remove(actividadEstudiante);
            return Save();
        }

        public ActividadEstudiante GetActividadEstudiante(int aeId)
        {
            return _context.ActividadEstudiantes.Where(a => a.Id == aeId).FirstOrDefault();
        }

        public ICollection<ActividadEstudiante> GetActividadEstudiantes()
        {
            return _context.ActividadEstudiantes.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateActividadEstudiante(int aeId,/*int actividadId, int estudianteId,*/ ActividadEstudiante actividadEstudiante)
        {
            _context.Update(actividadEstudiante);
            return Save();
        }
    }
}
