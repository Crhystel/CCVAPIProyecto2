using AutoMapper;
using CCVAPIProyecto2.Dto;
using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Estudiante, EstudianteDto>();
            CreateMap<EstudianteDto, Estudiante>();
            CreateMap<Profesor, ProfesorDto>();
            CreateMap<ProfesorDto, Profesor>();
            CreateMap<ClaseDto, Clase>();
            CreateMap<Clase, ClaseDto>();
            CreateMap<ClaseActividad, ClaseActividadDto>();
            CreateMap<ClaseActividadDto, ClaseActividad>();
            CreateMap<ClaseEstudiante, ClaseEstudianteDto>();
            CreateMap<ClaseEstudianteDto, ClaseEstudiante>();
            CreateMap<ClaseProfesor, ClaseProfesorDto>();
            CreateMap<ClaseProfesorDto, ClaseProfesor>();
            CreateMap<ActividadEstudiante, ActividadEstudianteDto>();
            CreateMap<ActividadEstudianteDto, ActividadEstudiante>();
            CreateMap<Actividad, ActividadDto>();
            CreateMap<ActividadDto, Actividad>();
            CreateMap<ActividadProfesor, ActividadProfesorDto>();
            CreateMap<ActividadProfesorDto, ActividadProfesor>();

        }
    }
}
