using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IClaseActividad
    {
        ICollection<ClaseActividad> GetClaseActividades();
        ClaseActividad GetClaseActividad(int caId);
        bool ClaseActividadExiste(int caId);
        bool CreateClaseActividad(ClaseActividad claseActividad);
        bool UpdateClaseActividad(ClaseActividad claseActividad);
        bool DeleteClaseActividad(ClaseActividad claseActividad);
        bool Save();
    }
}
