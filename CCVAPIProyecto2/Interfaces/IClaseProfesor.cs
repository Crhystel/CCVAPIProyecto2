﻿using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface IClaseProfesor
    {
        ICollection<ClaseProfesor> GetClaseProfesores();
        ClaseProfesor GetClaseProfesor(int cpId);
        bool ClaseProfesorExiste(int cpId);
        bool CreateClaseProfesor(int claseId, int profesorId);
        bool UpdateClaseProfesor(int cpId, ClaseProfesor claseProfesor);
        bool DeleteClaseProfesor(ClaseProfesor claseProfesor);
        bool Save();
    }
}
