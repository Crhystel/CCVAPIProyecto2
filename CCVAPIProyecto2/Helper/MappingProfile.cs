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
            CreateMap<ClaseDto, Clase>()
                .ForMember(dest => dest.ClaseEstudiantes, opt => opt.Ignore())
                .ForMember(dest => dest.ClaseProfesores, opt => opt.Ignore())
                .ForMember(dest => dest.ClaseActividades, opt => opt.Ignore());

            CreateMap<Clase, ClaseDto>();

            CreateMap<Actividad, ActividadDto>();
            CreateMap<ActividadDto, Actividad>();
            CreateMap<ActividadProfesor, ActividadProfesorDto>();
            CreateMap<ActividadProfesorDto, ActividadProfesor>();

        }
    }
}
