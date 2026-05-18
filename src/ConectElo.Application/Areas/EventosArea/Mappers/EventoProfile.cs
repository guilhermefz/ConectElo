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
            CreateMap<ExibirEventoDto, Evento>().ReverseMap();
            CreateMap<CriarEventoDto, Evento>().ReverseMap();
            CreateMap<EditarEventoDto, Evento>().ReverseMap();

            CreateMap<AniversarioEvento, ExibirAniversarioDto>().ReverseMap();
            CreateMap<AmigoSecretoEvento, ExibirAmigoSecretoDto>().ReverseMap();
            CreateMap<ListaDesejos, ExibirListaDesejosDto>().ReverseMap();
            CreateMap<ItensListaDesejos, ExibirItemListaDesejosDto>().ReverseMap();
        }
    }
}
