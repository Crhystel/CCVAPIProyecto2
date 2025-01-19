using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IActividadEstudiante
    {
        ICollection<ActividadEstudiante> GetActividadEstudiantes();
        ActividadEstudiante GetActividadEstudiante(int aeId);
        bool ActividadEstudianteExiste(int aeId);
        bool CreateActividadEstudiante(int actividadId, int estudianteId);
        bool UpdateActividadEstudiante(int aeId,/*int actividadId,int estudianteId, */ActividadEstudiante actividadEstudiante);
        bool DeleteActividadEstudiante(ActividadEstudiante actividadEstudiante);
        bool Save();
    }
}
