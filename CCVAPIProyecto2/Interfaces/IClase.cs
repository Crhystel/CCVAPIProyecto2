using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IClase
    {
        ICollection<Clase> GetClases();
        Clase GetClase(int id);
        bool ClaseExiste(int id);
        bool CreateClase(string nombre, /*int estudiantesId, int profesoresId, */Clase clase);
        bool UpdateClase(string nombre,/* int estudiantesId, int profesoresId,*/ Clase clase);
        bool DeleteClase(Clase clase);

        bool Save();
    }
}
