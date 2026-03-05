using AutoMapper;
using ConectElo.Application.Areas.EventosArea.DTOs;
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
        }
    }
}
