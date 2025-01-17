using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IProfesor
    {
        ICollection<Profesor> GetProfesores();
        Profesor GetProfesor(int id);
        bool CreateProfesor(MateriaEnum materiaId, Profesor profesor);
        bool UpdateProfesor(MateriaEnum materiaId, Profesor profesor);
        bool DeleteProfesor(Profesor profesor);
        bool ProfesorExiste(int id);
        bool Save();
    }
}
