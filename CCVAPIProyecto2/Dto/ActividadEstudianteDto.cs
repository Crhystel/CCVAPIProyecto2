namespace CCVAPIProyecto2.Dto
{
    public class ActividadEstudianteDto
    {
        public int Id { get; set; }
        public int ActividadId { get; set; }
        public int EstudianteId { get; set; }

        public string ActividadTitulo { get; set; }
        public string EstudianteNombre { get; set; }
        public DateTime FechaAsignacion { get; set; }

    }
}
