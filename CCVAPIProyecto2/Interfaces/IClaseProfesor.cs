using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IClaseProfesor
    {
        ICollection<ClaseProfesor> GetClaseProfesores();
        ICollection<ClaseProfesor> GetClasesByProfesorId(int profesorId);
        ClaseProfesor GetClaseProfesor(int cpId);
        bool ClaseProfesorExiste(int cpId);
        bool CreateClaseProfesor(ClaseProfesor claseProfesor);
        bool UpdateClaseProfesor(ClaseProfesor claseProfesor);
        bool DeleteClaseProfesor(ClaseProfesor claseProfesor);
        bool Save();
    }
}
