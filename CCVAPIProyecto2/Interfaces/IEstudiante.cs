using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IEstudiante
    {
        ICollection<Estudiante> GetEstudiantes();
        Estudiante GetEstudiante(int id);
        bool EstudianteExiste(int id);
        bool DeleteEstudiante(Estudiante estudiante);
        bool Save();
        bool CreateEstudiante( Estudiante estudiante);
        bool UpdateEstudiante(Estudiante estudiante);
    }
}
