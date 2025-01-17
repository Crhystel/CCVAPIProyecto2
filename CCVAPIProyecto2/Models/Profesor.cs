using System.ComponentModel.DataAnnotations;

namespace CCVAPIProyecto2.Models
{
    public class Profesor : Usuario
    {
        public MateriaEnum Materia { get; set; }
        public ICollection<ClaseProfesor> ClaseProfesores { get; set; }
        public ICollection<ActividadProfesor> ActividadProfesores { get; set; }
    }
}
