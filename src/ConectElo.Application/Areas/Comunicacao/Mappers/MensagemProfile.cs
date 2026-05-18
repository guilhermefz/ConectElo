using AutoMapper;
using ConectElo.Application.Areas.Comunicacao.DTOs;
using ConectElo.Domain.Areas.Comunicacao.Entities;

namespace ConectElo.Application.Areas.Comunicacao.Mappers
{
    public class MensagemProfile : Profile
    {
        public MensagemProfile()
        {
            CreateMap<Mensagem, MensagemDto>()
                .ForMember(dest => dest.NomeAutor, opt => opt.MapFrom(src => src.Autor.Nome));
        }
    }
}
