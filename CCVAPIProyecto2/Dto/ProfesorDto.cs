using CCVAPIProyecto2.Models;
using System.Text.Json.Serialization;

namespace CCVAPIProyecto2.Dto
{
    public class ProfesorDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public MateriaEnum Materia { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Cedula { get; set; }
        public string Contrasenia { get; set; }
        public string NombreUsuario { get; set; }
    }
}
