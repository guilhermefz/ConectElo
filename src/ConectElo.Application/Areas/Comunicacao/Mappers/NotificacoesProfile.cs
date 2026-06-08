using AutoMapper;
using ConectElo.Application.Areas.Comunicacao.DTOs;
using ConectElo.Domain.Areas.Comunicacao.Entities;

namespace ConectElo.Application.Areas.Comunicacao.Mappers
{
    public class NotificacoesProfile : Profile
    {
        public NotificacoesProfile()
        {
            CreateMap<Notificacoes, ExibirNotificacaoDto>().ReverseMap();
        }
    }
}
