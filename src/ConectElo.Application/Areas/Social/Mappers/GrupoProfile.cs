using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.Mappers
{
    public class GrupoProfile : Profile
    {
        public GrupoProfile() 
        {
            CreateMap<CriarGrupoDto, Grupo>().ReverseMap();

            CreateMap<BuscarGrupoDto, Grupo>().ReverseMap();

            CreateMap<EditarGrupoDto, Grupo>().ReverseMap();

            CreateMap<Grupo, GrupoExibicaoDto>()
                .ForMember(dest => dest.ImgGrupo, opt => opt.MapFrom(src => src.ImgGrupo));
        }
    }
}
