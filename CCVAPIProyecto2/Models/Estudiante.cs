using System.ComponentModel.DataAnnotations;

namespace CCVAPIProyecto2.Models
{
    public class Estudiante : Usuario
    {
        public GradoEnum Grado { get; set; }
        public ICollection<ClaseEstudiante> ClaseEstudiantes { get; set; }
        public ICollection<ActividadEstudiante> ActividadEstudiantes { get; set; }


    }
}
