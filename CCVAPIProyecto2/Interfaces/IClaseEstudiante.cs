using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IClaseEstudiante
    {
        ICollection<ClaseEstudiante> GetClaseEstudiantes();
        ClaseEstudiante GetClaseEstudiante(int ceId);
        bool ClaseEstudianteExiste(int ceId);
        bool CreateClaseEstudiante(ClaseEstudiante claseEstudiante);
        bool UpdateClaseEstudiante(ClaseEstudiante claseEstudiante);
        bool DeleteClaseEstudiante(ClaseEstudiante claseEstudiante);
        bool Save();
    }
}
