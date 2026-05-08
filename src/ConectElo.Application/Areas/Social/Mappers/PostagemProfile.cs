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

            CreateMap<Postagens, FeedPostagemDto>()
                .ForMember(dest => dest.NomeAutor, opt => opt.MapFrom(src => src.Autor.Nome))
                .ForMember(dest => dest.FotoPerfilUrl, opt => opt.MapFrom(src => src.Autor.FotoPerdilUrl))
                .ForMember(dest => dest.GrupoId, opt => opt.Ignore())
                .ForMember(dest => dest.NomeGrupo, opt => opt.Ignore());
        }
    }
}
