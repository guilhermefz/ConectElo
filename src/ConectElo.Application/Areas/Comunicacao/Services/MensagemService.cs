using AutoMapper;
using ConectElo.Application.Areas.Comunicacao.DTOs;
using ConectElo.Application.Areas.Comunicacao.InterfacesService;
using ConectElo.Domain.Areas.Comunicacao.Entities;
using ConectElo.Domain.Areas.Comunicacao.Enuns;
using ConectElo.Domain.Areas.Comunicacao.InterfacesRepository;
using ConectElo.Domain.Areas.Social.InterfacesRepository;

namespace ConectElo.Application.Areas.Comunicacao.Services
{
    public class MensagemService : IMensagemService
    {
        private readonly IMensagemRepository _mensagemRepository;
        private readonly IMembrosGrupoRepository _membrosGrupoRepository;
        private readonly IMapper _mapper;

        public MensagemService (IMensagemRepository mensagemRepository, IMembrosGrupoRepository membrosGrupoRepository, IMapper mapper)
        {
            _mensagemRepository = mensagemRepository;
            _membrosGrupoRepository = membrosGrupoRepository;
            _mapper = mapper;
        }

        public async Task<MensagemDto> EnviarMensagemAsync(Guid grupoId, Guid usuarioId, string conteudo)
        {
            var ehMembro = await _membrosGrupoRepository.VerificarMembroASync(grupoId, usuarioId);

            if (!ehMembro)
                throw new UnauthorizedAccessException("Você não tem acesso a este grupo");

            var mensagem = new Mensagem
            {
                GrupoId = grupoId,
                UsuarioId = usuarioId,
                Conteudo = conteudo,
                HorarioEnvio = DateTime.UtcNow,
                TipoMidia = TipoMidiaMensagem.Texto
            };

            await _mensagemRepository.Inserir(mensagem);
            await _mensagemRepository.CommitAsync();

            var mensagemDto = _mapper.Map<MensagemDto>(mensagem);
            return mensagemDto;
        }

        public async Task<IEnumerable<MensagemDto>> ObterHistoricoAsync(Guid grupoId, Guid usuarioId)
        {
            var ehMembro = await _membrosGrupoRepository.VerificarMembroASync(grupoId, usuarioId);

            if (!ehMembro)
                throw new UnauthorizedAccessException("Você não tem acesso a este grupo");

            var mensagens = await _mensagemRepository.ObterMensagensDoGrupoAsync(grupoId);

            return mensagens.Select(mensagem => new MensagemDto
            {
                Id = mensagem.Id,
                Conteudo = mensagem.Conteudo,
                NomeAutor = mensagem.Autor.Nome,
                UsuarioId = mensagem.UsuarioId,
                HorarioEnvio = mensagem.HorarioEnvio,
            });
        }
    }
}
