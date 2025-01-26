using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.EntityFrameworkCore;

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

        public bool CreateClase(Clase clase)
        {
            _context.Add(clase);
            return Save();
        }



        public bool DeleteClase(Clase clase)
        {
            //eliminar relaciones con claves foraneas
            var actividadesRelacionadas = _context.ClaseActividades.Where(ca => ca.ClaseId == clase.Id);
            _context.RemoveRange(actividadesRelacionadas);
            _context.Clases.Remove(clase);
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

        public bool UpdateClase(Clase clase)
        {
            var trackedEntity = _context.Clases.Local.FirstOrDefault(e => e.Id == clase.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
            _context.Attach(clase);
            _context.Entry(clase).State = EntityState.Modified;
            return Save();
        }
    }
}
