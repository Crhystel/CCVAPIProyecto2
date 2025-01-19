using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IClaseActividad
    {
        ICollection<ClaseActividad> GetClaseActividades();
        ClaseActividad GetClaseActividad(int claseId, int actividadId);
        bool ClaseActividadExiste(int claseId, int actividadId);
        bool CreateClaseActividad(int claseId, int actividadId/*, ClaseActividad claseActividad*/);
        bool UpdateClaseActividad(int claseId, int actividadId, ClaseActividad claseActividad);
        bool DeleteClaseActividad(ClaseActividad claseActividad);
        bool Save();
    }
}
