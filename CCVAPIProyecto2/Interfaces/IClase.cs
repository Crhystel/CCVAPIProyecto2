using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IClase
    {
        ICollection<Clase> GetClases();
        Clase GetClase(int id);
        bool ClaseExiste(int id);
        bool CreateClase(Clase clase);
        bool UpdateClase(Clase clase);
        bool DeleteClase(Clase clase);

        bool Save();
    }
}
