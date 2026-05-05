using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.Mappers
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, RegistrarUsuarioDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<EditarUsuarioDto, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UltimaAtualizacao, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<PerfilUsuarioDto, Usuario>().ReverseMap();
        }
    }
}
