using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.Mappers
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, RegistrarUsuarioDto>()
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
        }
    }
}
