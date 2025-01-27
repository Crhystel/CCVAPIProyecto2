using System.ComponentModel.DataAnnotations;

namespace CCVAPIProyecto2.Models
{
    public class ActividadEstudiante
    {
        [Key]
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        public DateTime FechaAsignacion { get; set; } = DateTime.Now;
        public int ActividadId { get; set; }
        public Actividad Actividad { get; set; }
    }
}
