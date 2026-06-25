using AutoMapper;
using ConectElo.Application.Areas.AmigoSecreto.DTOs;
using ConectElo.Domain.Areas.Dinamicas.Entities;

namespace ConectElo.Application.Areas.AmigoSecreto.Mappers
{
    public class AmigoSecretoProfile : Profile
    {
        public AmigoSecretoProfile()
        {
             CreateMap<MensagemAnonima, MensagemAnonimaDto>();
        }
    }
}
