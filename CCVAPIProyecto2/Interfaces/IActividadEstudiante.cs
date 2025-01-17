using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IActividadEstudiante
    {
        ICollection<ActividadEstudiante> GetActividadEstudiantes();
        ActividadEstudiante GetActividadEstudiante(int aeId);
        ActividadEstudiante GetActividadEstudiantePorEstudiante(int estudianteId);
        ActividadEstudiante GetActividadEstudiantePorActividad(int actividadId);
        bool ActividadEstudianteExiste(int aeid);
    }
}
