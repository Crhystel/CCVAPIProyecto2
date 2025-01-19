using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IActividadProfesor
    {
        ICollection<ActividadProfesor> GetActividadProfesores();
        ActividadProfesor GetActividadProfesor(int apId);
        bool ActividadProfesorExiste(int apId);
        bool CreateActividadProfesor(int actividadId, int profesorId);
        bool UpdateActividadProfesor(int apId,ActividadProfesor actividadProfesor);
        bool DeleteActividadProfesor(ActividadProfesor actividadProfesor);
        bool Save();
    }
}
