using AutoMapper;
using ConectElo.Application.Areas.Comunicacao.DTOs;
using ConectElo.Application.Areas.Comunicacao.InterfacesService;
using ConectElo.Domain.Areas.Comunicacao.Entities;
using ConectElo.Domain.Areas.Comunicacao.Enuns;
using ConectElo.Domain.Areas.Comunicacao.InterfacesRepository;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Domain.Exceptions;

namespace ConectElo.Application.Areas.Comunicacao.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacoesRepository _notificacaoRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMembrosGrupoRepository _membrosGrupoRepository;
        private readonly IMapper _mapper;

        public NotificacaoService(INotificacoesRepository notificacaoRepository, IGrupoRepository grupoRepository, IMembrosGrupoRepository membrosGrupoRepository, IMapper mapper)
        {
            _notificacaoRepository = notificacaoRepository;
            _grupoRepository = grupoRepository;
            _membrosGrupoRepository = membrosGrupoRepository;
            _mapper = mapper;
        }

        public async Task<List<ExibirNotificacaoDto>> ListarPorUsuario(Guid usuarioId)
        {
            var notificacoes = await _notificacaoRepository.ListarPorUsuario(usuarioId);
            return _mapper.Map<List<ExibirNotificacaoDto>>(notificacoes);
        }

        public async Task MarcarComoLida(Guid avisoId, Guid usuarioId)
        {
            var notificacao = await _notificacaoRepository.SelecionarPorId(avisoId);

            if (notificacao is null)
                throw new NotFoundException("Aviso não encontrado.");
            if (notificacao.UsuarioId != usuarioId)
                throw new UnathorizedException("Você não pode alterar este aviso.");

            notificacao.NotificacaoLida = true;
            await _notificacaoRepository.Atualizar(notificacao);
        }

        public async Task<List<ExibirNotificacaoDto>> CriarNotificacoesNovoEvento(Guid eventoId, Guid grupoId, string nomeEvento, string nomeCriador, Guid criadorId)
        {
            var grupo = await _grupoRepository.SelecionarPorId(grupoId);
            if (grupo is null) return new();

            var membros = await _membrosGrupoRepository.ListarPorGrupo(grupoId);

            var notificacoes = membros
                .Where(m => m.UsuarioId != criadorId)
                .Select(m => new Notificacoes
                {
                    UsuarioId = m.UsuarioId,
                    Conteudo = $"{nomeCriador} criou o evento \"{nomeEvento}\" no grupo {grupo.Nome}.",
                    LinkUrl = $"/eventos/{eventoId}",
                    NotificacaoLida = false,
                    DataEnvio = DateTime.UtcNow,
                    TipoNotificacao = TipoNotificacaoEnum.Social
                }).ToList();

            foreach (var notificacao in notificacoes)
                await _notificacaoRepository.Inserir(notificacao);

            var notificacaoModel  = _mapper.Map<List<ExibirNotificacaoDto>>(notificacoes);

            return notificacaoModel;
        }
    }
}
