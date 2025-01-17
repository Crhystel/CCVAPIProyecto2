using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Repositories
{
    public class ClaseRepository : IClase
    {
        private readonly DataContext _context;
        public ClaseRepository(DataContext context)
        {
            _context = context;
        }
        public bool ClaseExiste(int id)
        {
            return _context.Clases.Any(c => c.Id == id);
        }

        public bool CreateClase(string nombre, /*int estudiantesId, int profesoresId, */Clase clase)
        {
            var nombreClase = _context.Clases.SingleOrDefault(c => c.Nombre==nombre);
            //var estudianteClase = _context.Estudiantes.SingleOrDefault(e => e.Id == estudiantesId);
            //var profesorClase = _context.Profesores.SingleOrDefault(p => p.Id == profesoresId);

            _context.Add(clase);
            clase.Nombre = nombre;
           

            //var nuevoProfesorClase = new ClaseProfesor()
            //{
            //    ClaseP = nombreClase,
            //    Profesor = profesorClase,
            //};
            //_context.Add(nuevoProfesorClase);
            //var nuevoEstudianteClase = new ClaseEstudiante()
            //{
            //    Clase = nombreClase,
            //    Estudiante = estudianteClase
            //};
            //_context.Add(nuevoEstudianteClase);
            

            return Save();
        }



        public bool DeleteClase(Clase clase)
        {
            _context.Remove(clase);
            return Save();
        }

        public Clase GetClase(int id)
        {
            return _context.Clases.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Clase> GetClases()
        {
            return _context.Clases.OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClase(string nombre, /*int estudiantesId, int profesoresId, */Clase clase)
        {
            _context.Update(clase);
            return Save();
        }
    }
}
