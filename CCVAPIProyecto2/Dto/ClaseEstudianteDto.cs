using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Dto
{
    public class ClaseEstudianteDto
    {
        public int Id { get; set; }
        public int ClaseId { get; set; }
        public string ClaseNombre { get; set; }
        public int EstudianteId { get; set; }
        public string EstudianteNombre { get; set; }
    }
}
