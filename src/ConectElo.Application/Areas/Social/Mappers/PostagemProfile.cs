using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.Mappers
{
    public class PostagemProfile : Profile
    {
        public PostagemProfile() 
        {
            CreateMap<EditarPostagemDto, Postagens>().ReverseMap();
            CreateMap<CriarPostagemDto, Postagens>().ReverseMap();
            CreateMap<ExibirPostagemDto, Postagens>().ReverseMap();
        }
    }
}
