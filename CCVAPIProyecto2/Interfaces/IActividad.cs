using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IActividad
    {
        ICollection<Actividad> GetActividades();
        Actividad GetActividad(int actividadId);
        bool ActividadExiste(int actividadId);
        public bool CreateActividad(int actividadId, int claseID, Actividad actividad);
        bool UpdateActividad(int actividadId, Actividad actividad);
        bool DeleteActividad(Actividad actividad);
        ICollection<Actividad> GetActividadesPorClase(int claseId);
        bool Save();
    }
}
