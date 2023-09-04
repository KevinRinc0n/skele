using APIIncidencias.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace APIIncidencias.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pais, PaisDto>().ReverseMap();

        CreateMap<Pais, PaisesDto>().ReverseMap();
 
        CreateMap<Departamento, DepartamentoDto>().ReverseMap(); 

        CreateMap<Departamento, DepartamentosDto>().ReverseMap();

        CreateMap<Ciudad, CiudadDto>().ReverseMap(); 

        CreateMap<Ciudad, CiudadesDto>().ReverseMap();

        CreateMap<Persona, PersonasDto>().ReverseMap();

        CreateMap<Persona, PersonaDto>().ReverseMap();

        CreateMap<Salon, SalonDto>().ReverseMap();

        CreateMap<Salon, SalonesDto>().ReverseMap();

        CreateMap<Genero, GenerosDto>().ReverseMap();

        CreateMap<Genero, GeneroDto>().ReverseMap();

        CreateMap<Matricula, MatriculaDto>().ReverseMap();       

        CreateMap<TipoPersona, TipoPersonaDto>().ReverseMap();        
    }
}