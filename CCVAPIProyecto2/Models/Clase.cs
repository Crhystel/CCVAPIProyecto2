using System.ComponentModel.DataAnnotations;

namespace CCVAPIProyecto2.Models
{
    public class Clase
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<ClaseEstudiante> ClaseEstudiantes { get; set; }
        public ICollection<ClaseProfesor> ClaseProfesores { get; set; }
        public ICollection<ClaseActividad> ClaseActividades { get; set; }
    }
}
