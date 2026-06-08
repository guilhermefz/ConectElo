using AutoMapper;
using ConectElo.Application.Areas.Comunicacao.DTOs;
using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.Home.DTOs;
using ConectElo.Application.Areas.Social.DTOs;

namespace ConectElo.Application.Areas.Home.Mappers
{
    public class HomeProfile : Profile
    {
        public HomeProfile()
        {
            CreateMap<ExibirEventoDto, ProximoEventoDto>()
                .ForMember(dest => dest.MinhaConfirmacao, opt => opt.MapFrom(src => src.ParticipacaoUsuario))
                .ForMember(dest => dest.DiasRestantes, opt => opt.MapFrom(src => src.DataInicio.HasValue ? (int)(src.DataInicio.Value.Date - DateTime.UtcNow.Date).TotalDays : 0))
                .ForMember(dest => dest.NomeGrupo, opt => opt.Ignore());

            CreateMap<GrupoExibicaoDto, GrupoResumoDto>()
                .ForMember(dest => dest.FotoUrl, opt => opt.MapFrom(src => src.ImgGrupo));

            CreateMap<ExibirNotificacaoDto, AtividadeRecenteDto>();
        }
    }
}
