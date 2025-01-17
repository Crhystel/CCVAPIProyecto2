using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IAdministrador
    {
        ICollection<Profesor> GetProfesores();
        ICollection<Estudiante> GetEstudiantes();
        ICollection<Clase> GetClases();
        ICollection<Actividad> GetActividades();
    }
}
