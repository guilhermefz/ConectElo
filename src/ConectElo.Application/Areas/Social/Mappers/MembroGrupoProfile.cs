using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.Mappers
{
    public class MembroGrupoProfile : Profile
    {
        public MembroGrupoProfile()
        {
            CreateMap<MembrosGrupo, MembroGrupoExibicaoDto>()
                .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<CriarMembroGrupoDto, MembrosGrupo>().ReverseMap();
        }
    }
}
