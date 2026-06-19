using AutoMapper;
using ConectElo.Application.Areas.AmigoSecreto.DTOs;
using ConectElo.Application.Areas.AmigoSecreto.InterfacesService;
using ConectElo.Application.Areas.AmigoSecreto.Utils;
using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Dinamicas.Enuns;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Domain.Exceptions;

namespace ConectElo.Application.Areas.AmigoSecreto.Services
{
    public class AmigoSecretoService : IamigoSecretoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IConfirmacaoEventoRepository _confirmacaoEventoRepository;
        private readonly IResultadoSorteioRepository _resultadoSorteioRepository;
        private readonly IMensagemAnonimaRepository _mensagemAnonimaRepository;
        private readonly IMapper _mapper;

        public AmigoSecretoService(IEventoRepository eventoRepository, IConfirmacaoEventoRepository confirmacaoEventoRepository, IResultadoSorteioRepository resultadoSorteioRepository, IMensagemAnonimaRepository mensagemAnonimaRepository, IMapper mapper)
        {
            _eventoRepository = eventoRepository;
            _confirmacaoEventoRepository = confirmacaoEventoRepository;
            _resultadoSorteioRepository = resultadoSorteioRepository;
            _mensagemAnonimaRepository = mensagemAnonimaRepository;
            _mapper = mapper;
        }

        public async Task<string> AgendarSorteio(AgendarSorteioDto dto, Guid criadorId)
        {
            var evento = await BuscarAmigoSecreto(dto.EventoId);

            ValidarCriador(evento.Criador, criadorId);

            if (evento.Sorteado)
                throw new BusinessException("O sorteio já foi realizado e não pode ser reagendado.");

            if (dto.DataSorteio <= DateTime.UtcNow)
                throw new BusinessException("A data do sorteio deve ser no futuro.");

            //if (!string.IsNullOrEmpty(evento.HangfireJobId))
            //    BackgroundJob.Delete(evento.HangfireJobId);

            //var jobId = BackgroundJob.Schedule<IAmigoSecretoService>(
            //    s => s.ExecutarSorteio(dto.EventoId),
            //    dto.DataSorteio);

            evento.DataSorteio = dto.DataSorteio;
            //evento.HangfireHobId = jobId;
            evento.StatusSorteio = StatusSorteioEnum.SorteioAgendado;

            await _eventoRepository.Atualizar(evento);
            
            return null; //return jobId;
        }

        public async Task<SorteioExecutadoDto> ExecutarSorteio(Guid eventoId)
        {
            var evento = await BuscarAmigoSecreto(eventoId);

            if (evento.Sorteado)
                return new SorteioExecutadoDto
                {
                    EventoId = eventoId,
                    DataExecucao = evento.DataExecucaoSorteio!.Value,
                    TotalPares = 0,
                    ParticipantesIds = new()
                };

            var confirmados = await _confirmacaoEventoRepository.ListarPorEvento(eventoId);
            var participantes = confirmados
                    .Where(c => c.Status == Domain.Areas.Geral.Enuns.StatusConfirmacaoEventoEnum.Confirmado)
                    .Select(c => c.UsuarioId)
                    .ToList();

            if (participantes.Count < 2)
                throw new BusinessException("São necessários pelo menos 2 participantes confirmados para realizar o sorteio.");

            var pares = SorteioAlgoritmo.Sortear(participantes);

            var agora = DateTime.UtcNow;
            var resultados = pares.Select(par => new ResultadoSorteio
            {
                EventoId = eventoId,
                PresenteadorId = par.Presenteador,
                RecebedorId = par.Recebedor,
                DataSorteio = agora,
            }).ToList();

            foreach (var resultado in resultados)
                await _resultadoSorteioRepository.Inserir(resultado);

            evento.Sorteado = true;
            evento.StatusSorteio = StatusSorteioEnum.Sorteado;
            evento.DataExecucaoSorteio = agora;
            //evento.HangfireJobId = null;

            await _eventoRepository.Atualizar(evento);

            return new SorteioExecutadoDto
            {
                EventoId = eventoId,
                DataExecucao = agora,
                TotalPares = pares.Count,
                ParticipantesIds = participantes
            };
        }

        private async Task<AmigoSecretoEvento> BuscarAmigoSecreto(Guid eventoId)
        {
            var evento = await _eventoRepository.SelecionarPorId(eventoId);

            return evento as AmigoSecretoEvento ?? throw new NotFoundException("Evento de amigo secreto não encontrado.");
        }

        private static void ValidarCriador(Guid criadorDoEvento, Guid usuarioId)
        {
            if (criadorDoEvento != usuarioId)
                throw new UnathorizedException("Apenas o criador do evento pode realizar esta ação.");
        }

        public async Task<SorteioExecutadoDto> SortearAgora(Guid eventoId, Guid criadorId)
        {
            var evento = await BuscarAmigoSecreto(eventoId);

            ValidarCriador(evento.Criador, criadorId);

            if (evento.Sorteado)
                throw new BusinessException("O sorteio já foi realizado.");

            //if (!string.IsNullOrEmpty(evento.HangfireJobId))
            //{
            //    BackgroundJob.Delete(evento.HangfireJobId);
            //    evento.HangfireJobId = null;
                await _eventoRepository.Atualizar(evento);
            //}

            return await ExecutarSorteio(eventoId);
        }

        public async Task<string> AlterarDataSorteio(Guid eventoId, DateTime novaData, Guid criadorId)
        {
            var dto = new AgendarSorteioDto
            {
                EventoId = eventoId,
                DataSorteio = novaData
            };

            return await AgendarSorteio(dto, criadorId);
        }

        public async Task<MeuResultadoDto> BuscarMeuResultado(Guid eventoId, Guid usuarioId)
        {
            var evento = await BuscarAmigoSecreto(eventoId);

            if (!evento.Sorteado)
                throw new BusinessException("O sorteio ainda não foi realizado.");

            var comoPresenteador = await _resultadoSorteioRepository
                .BuscarComoPresenteador(eventoId, usuarioId);

            var comoRecebedor = await _resultadoSorteioRepository
                .BuscarComoRecebedor(eventoId, usuarioId);

            var resultado = new MeuResultadoDto();

            if (comoPresenteador is not null)
            {
                resultado.ComoPresenteador = new ResultadoComoPresenteadorDto
                {
                    ResultadoSorteioId = comoPresenteador.Id,
                    NomeRecebedor = comoPresenteador.Recebedor.Nome,
                    FotoRecebedor = comoPresenteador.Recebedor.FotoPerdilUrl,
                    //ListaDesejos = evento is AniversarioEvento aniversario && aniversario.ListaDesejos is not null ? _mapper.Map<ExibirListaDesejosDto>(aniversario.ListaDesejos) : null
                };
            }

            if (comoRecebedor is not null)
            {
                resultado.ComoRecebedor = new ResultadoComoRecebedorDto
                {
                    ResultadoSorteioId = comoRecebedor.Id
                };
            }

            return resultado;
        }

        public async Task<List<MensagemAnonimaDto>> BuscarHistorico(Guid resultadoSorteioId, Guid usuarioId)
        {
            await ValidarParticipanteDoResultado(resultadoSorteioId, usuarioId);

            var mensagens = ""; // await _mensagemAnonimaRepository.ListarPorResultado(resultadoSorteioId);

            return _mapper.Map<List<MensagemAnonimaDto>>(mensagens);
        }

        public async Task<MensagemAnonimaDto> EnviarMensagem(Guid resultadoSorteioId, Guid usuarioId, string conteudo)
        {
            var resultado = await _resultadoSorteioRepository
                .SelecionarPorId(resultadoSorteioId)
                ?? throw new NotFoundException("Resultado de sorteio não encontrado.");

            var participanteTipo = resultado.PresenteadorId == usuarioId
                ? ParticipanteTipoEnum.Presenteador
                : resultado.RecebedorId == usuarioId
                    ? ParticipanteTipoEnum.Recebedor
                    : throw new UnathorizedException(
                        "Você não faz parte deste par de amigo secreto.");

            var mensagem = new MensagemAnonima
            {
                ResultadoSorteioId = resultadoSorteioId,
                Conteudo = conteudo,
                HorarioEnvio = DateTime.UtcNow,
                ParticipanteTipo = participanteTipo
            };

            await _mensagemAnonimaRepository.Inserir(mensagem);

            return _mapper.Map<MensagemAnonimaDto>(mensagem);
        }

        private async Task ValidarParticipanteDoResultado(Guid resultadoSorteioId, Guid usuarioId)
        {
            var resultado = await _resultadoSorteioRepository.SelecionarPorId(resultadoSorteioId)
                ?? throw new NotFoundException("Resultado não encontrado.");

            if (resultado.PresenteadorId != usuarioId && resultado.RecebedorId != usuarioId)
                throw new UnathorizedException("Você não faz parte deste par de amigo secreto.");
        }

    }
}
