using CCVAPIProyecto2.Models;
using System.Text.Json.Serialization;

namespace CCVAPIProyecto2.Dto
{
    public class EstudianteDto
    {
        public int Id { get; set; }
        public GradoEnum Grado { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Cedula { get; set; }
        public string Contrasenia { get; set; }
        public string NombreUsuario { get; set; }
        [JsonIgnore]
        public RolEnum Rol { get; set; } = RolEnum.Estudiante;


    }
}
