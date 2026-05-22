using AutoMapper;
using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.Social.DTOs.EventosDTO;
using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.Entities;

namespace ConectElo.Application.Areas.EventosArea.Mappers
{
    public class EventoProfile : Profile
    {
        public EventoProfile()
        {
            CreateMap<Evento, ExibirEventoDto>()
                .Include<AniversarioEvento, ExibirAniversarioDto>()
                .Include<AmigoSecretoEvento, ExibirAmigoSecretoDto>()
                .ForMember(dest => dest.CriadorNome, opt => opt.MapFrom(src => src.CriadorEvento != null ? src.CriadorEvento.Nome : null));

            CreateMap<CriarEventoDto, Evento>().ReverseMap();
            CreateMap<EditarEventoDto, Evento>().ReverseMap();

            CreateMap<AniversarioEvento, ExibirAniversarioDto>()
                .ForMember(dest => dest.CriadorNome, opt => opt.MapFrom(src => src.CriadorEvento != null ? src.CriadorEvento.Nome : null))
                .ReverseMap();

            CreateMap<AmigoSecretoEvento, ExibirAmigoSecretoDto>().ReverseMap();
            CreateMap<ListaDesejos, ExibirListaDesejosDto>().ReverseMap();
            CreateMap<ItensListaDesejos, ExibirItemListaDesejosDto>().ReverseMap();
        }
    }
}
