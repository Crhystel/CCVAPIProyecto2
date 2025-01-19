using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IClaseEstudiante
    {
        ICollection<ClaseEstudiante> GetClaseEstudiantes();
        ClaseEstudiante GetClaseEstudiante(int ceId);
        bool ClaseEstudianteExiste(int ceId);
        bool CreateClaseEstudiante(int claseId, int estudianteId);
        bool UpdateClaseEstudiante(int ceId, ClaseEstudiante claseEstudiante);
        bool DeleteClaseEstudiante(ClaseEstudiante claseEstudiante);
        bool Save();
    }
}
