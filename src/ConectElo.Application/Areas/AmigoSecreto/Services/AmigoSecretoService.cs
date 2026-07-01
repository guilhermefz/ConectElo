using AutoMapper;
using ConectElo.Application.Areas.AmigoSecreto.DTOs;
using ConectElo.Application.Areas.AmigoSecreto.InterfacesService;
using ConectElo.Application.Areas.AmigoSecreto.Utils;
using ConectElo.Application.Areas.Comunicacao.InterfacesService;
using ConectElo.Application.Areas.Social.DTOs.EventosDTO;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Dinamicas.Enuns;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Domain.Areas.Geral.Enuns;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Domain.Exceptions;
using Hangfire;

namespace ConectElo.Application.Areas.AmigoSecreto.Services
{
    public class AmigoSecretoService : IAmigoSecretoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IConfirmacaoEventoRepository _confirmacaoEventoRepository;
        private readonly IResultadoSorteioRepository _resultadoSorteioRepository;
        private readonly IMensagemAnonimaRepository _mensagemAnonimaRepository;
        private readonly IListaDesejosRepository _listaDesejosRepository;
        private readonly IItensListaDesejosRepository _itensListaDesejosRepository;
        private readonly IPerguntaQuizRepository _perguntaQuizRepository;
        private readonly IPerguntaAmigoSecretoRepository _perguntaAmigoSecretoRepository;
        private readonly IInteresseRepository _interesseRepository;
        private readonly INotificacaoService _notificacaoService;
        private readonly IMapper _mapper;

        private const int SlotsQuizMaximo = 3;

        public AmigoSecretoService(IEventoRepository eventoRepository, IConfirmacaoEventoRepository confirmacaoEventoRepository, IResultadoSorteioRepository resultadoSorteioRepository, IMensagemAnonimaRepository mensagemAnonimaRepository, IListaDesejosRepository listaDesejosRepository, IItensListaDesejosRepository itensListaDesejosRepository, IPerguntaQuizRepository perguntaQuizRepository, IPerguntaAmigoSecretoRepository perguntaAmigoSecretoRepository, IInteresseRepository interesseRepository, INotificacaoService notificacaoService, IMapper mapper)
        {
            _eventoRepository = eventoRepository;
            _confirmacaoEventoRepository = confirmacaoEventoRepository;
            _resultadoSorteioRepository = resultadoSorteioRepository;
            _mensagemAnonimaRepository = mensagemAnonimaRepository;
            _listaDesejosRepository = listaDesejosRepository;
            _itensListaDesejosRepository = itensListaDesejosRepository;
            _perguntaQuizRepository = perguntaQuizRepository;
            _perguntaAmigoSecretoRepository = perguntaAmigoSecretoRepository;
            _interesseRepository = interesseRepository;
            _notificacaoService = notificacaoService;
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

            if (!string.IsNullOrEmpty(evento.HangfireJobId))
                BackgroundJob.Delete(evento.HangfireJobId);

            var jobId = BackgroundJob.Schedule<IAmigoSecretoService>(
                s => s.ExecutarSorteio(dto.EventoId),
                dto.DataSorteio);

            evento.DataSorteio = dto.DataSorteio;
            evento.HangfireJobId = jobId;
            evento.StatusSorteio = StatusSorteioEnum.SorteioAgendado;

            await _eventoRepository.Atualizar(evento);
            
            return jobId;
        }

        public async Task<SorteioExecutadoDto> Sortear(Guid eventoId, Guid criadorId)
        {
            var evento = await BuscarAmigoSecreto(eventoId);

            ValidarCriador(evento.Criador, criadorId);

            if (evento.Sorteado)
                throw new BusinessException("O sorteio já foi realizado.");

            var confirmados = await _confirmacaoEventoRepository.ListarPorEvento(eventoId);
            var participantes = confirmados
                    .Where(c => c.Status == StatusConfirmacaoEventoEnum.Confirmado)
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

            await _eventoRepository.Atualizar(evento);

            return new SorteioExecutadoDto
            {
                EventoId = eventoId,
                DataExecucao = agora,
                TotalPares = pares.Count,
                ParticipantesIds = participantes
            };
        }

        public async Task<SorteioExecutadoDto> ExecutarSorteio(Guid eventoId) //obsoleto
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
                    .Where(c => c.Status == StatusConfirmacaoEventoEnum.Confirmado)
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
            evento.HangfireJobId = null;

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

            if (!string.IsNullOrEmpty(evento.HangfireJobId))
            {
                BackgroundJob.Delete(evento.HangfireJobId);
                evento.HangfireJobId = null;
                await _eventoRepository.Atualizar(evento);
            }

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
                var listaDoRecebedor = await _listaDesejosRepository
                    .BuscarPorEventoEUsuario(eventoId, comoPresenteador.RecebedorId);

                resultado.ComoPresenteador = new ResultadoComoPresenteadorDto
                {
                    ResultadoSorteioId = comoPresenteador.Id,
                    NomeRecebedor = comoPresenteador.Recebedor?.Nome ?? "Desconhecido",
                    FotoRecebedor = comoPresenteador.Recebedor?.FotoPerdilUrl,
                    ListaDesejos = listaDoRecebedor is not null
                        ? _mapper.Map<ExibirListaDesejosDto>(listaDoRecebedor)
                        : null
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

            var mensagens =  await _mensagemAnonimaRepository.ListarPorResultado(resultadoSorteioId);

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

        public async Task<ExibirListaDesejosDto> BuscarMinhaLista(Guid eventoId, Guid usuarioId)
        {
            var lista = await ObterOuCriarLista(eventoId, usuarioId);
            return _mapper.Map<ExibirListaDesejosDto>(lista);
        }

        public async Task<ExibirItemListaDesejosDto> AdicionarItemMinhaLista(Guid eventoId, Guid usuarioId, CriarItemListaDesejosDto dto)
        {
            var lista = await ObterOuCriarLista(eventoId, usuarioId);

            var item = new ItensListaDesejos
            {
                Descricao = dto.Descricao,
                UrlReference = dto.UrlReference ?? string.Empty,
                ListaDesejosId = lista.Id
            };

            await _itensListaDesejosRepository.Inserir(item);
            return _mapper.Map<ExibirItemListaDesejosDto>(item);
        }

        public async Task RemoverItemMinhaLista(Guid itemId, Guid usuarioId)
        {
            var item = await _itensListaDesejosRepository.BuscarPorId(itemId)
                ?? throw new NotFoundException("Item não encontrado.");

            var lista = await _listaDesejosRepository.SelecionarPorId(item.ListaDesejosId);

            if (lista?.UsuarioId != usuarioId)
                throw new UnathorizedException("Você só pode remover itens da sua própria lista.");

            await _itensListaDesejosRepository.Excluir(item);
        }

        private async Task<ListaDesejos> ObterOuCriarLista(Guid eventoId, Guid usuarioId)
        {
            var lista = await _listaDesejosRepository.BuscarPorEventoEUsuario(eventoId, usuarioId);

            if (lista is null)
            {
                lista = new ListaDesejos
                {
                    Titulo = "Minha lista de desejos",
                    EventoId = eventoId,
                    UsuarioId = usuarioId
                };
                await _listaDesejosRepository.Inserir(lista);
            }

            return lista;
        }

        // ─────────────────────────── Detalhe + Quiz ───────────────────────────

        public async Task<AmigoSecretoDetalheDto> BuscarDetalhe(Guid eventoId, Guid usuarioId)
        {
            var evento = await BuscarAmigoSecreto(eventoId);

            if (!evento.Sorteado)
                throw new BusinessException("O sorteio ainda não foi realizado.");

            var comoPresenteador = await _resultadoSorteioRepository.BuscarComoPresenteador(eventoId, usuarioId)
                ?? throw new NotFoundException("Você ainda não possui um amigo secreto neste evento.");

            var recebedor = await _interesseRepository.ObterUsuarioComInteresses(comoPresenteador.RecebedorId)
                ?? throw new NotFoundException("Recebedor não encontrado.");

            var lista = await _listaDesejosRepository
                .BuscarPorEventoEUsuario(eventoId, comoPresenteador.RecebedorId);

            var perguntasAtivas = await _perguntaAmigoSecretoRepository
                .ListarAtivasPorResultado(comoPresenteador.Id);

            var catalogo = await _perguntaQuizRepository.ListarAtivasComOpcoes();

            return new AmigoSecretoDetalheDto
            {
                ResultadoSorteioId = comoPresenteador.Id,
                NomeRecebedor = recebedor.Nome,
                FotoRecebedor = recebedor.FotoPerdilUrl,
                Bio = recebedor.Bio,
                Idade = CalcularIdade(recebedor.DataNascimento),
                Genero = recebedor.Genero,
                Interesses = recebedor.Interesses
                    .Select(i => new InteresseDto { Id = i.Id, Nome = i.Nome })
                    .ToList(),
                ListaDesejos = lista is not null
                    ? _mapper.Map<ExibirListaDesejosDto>(lista)
                    : null,
                Valor = evento.Valor,
                DataSorteio = evento.DataSorteio,
                SlotsTotais = SlotsQuizMaximo,
                SlotsUsados = perguntasAtivas.Count,
                PerguntasAtivas = perguntasAtivas.Select(MapPerguntaAtiva).ToList(),
                PerguntasDisponiveis = _mapper.Map<List<PerguntaCatalogoDto>>(catalogo)
            };
        }

        public async Task<List<PerguntaCatalogoDto>> ListarCatalogoQuiz()
        {
            var catalogo = await _perguntaQuizRepository.ListarAtivasComOpcoes();
            return _mapper.Map<List<PerguntaCatalogoDto>>(catalogo);
        }

        public async Task<PerguntarQuizResultadoDto> PerguntarQuiz(Guid eventoId, Guid usuarioId, Guid perguntaQuizId)
        {
            var comoPresenteador = await _resultadoSorteioRepository.BuscarComoPresenteador(eventoId, usuarioId)
                ?? throw new NotFoundException("Você ainda não possui um amigo secreto neste evento.");

            var ativas = await _perguntaAmigoSecretoRepository.ContarAtivasPorResultado(comoPresenteador.Id);
            if (ativas >= SlotsQuizMaximo)
                throw new BusinessException($"Você só pode manter {SlotsQuizMaximo} perguntas ativas por vez. Troque uma pergunta existente.");

            var pergunta = await _perguntaQuizRepository.BuscarComOpcoes(perguntaQuizId);
            if (pergunta is null || !pergunta.Ativa)
                throw new NotFoundException("Pergunta não encontrada.");

            if (await _perguntaAmigoSecretoRepository.ExisteAtivaComPergunta(comoPresenteador.Id, perguntaQuizId))
                throw new BusinessException("Você já fez essa pergunta.");

            var nova = new PerguntaAmigoSecreto
            {
                ResultadoSorteioId = comoPresenteador.Id,
                PerguntaQuizId = perguntaQuizId,
                Status = StatusPerguntaEnum.Ativa,
                PerguntadaEm = DateTime.UtcNow
            };

            await _perguntaAmigoSecretoRepository.Inserir(nova);
            nova.PerguntaQuiz = pergunta;

            var notificacao = await _notificacaoService
                .CriarNotificacaoPerguntaAmigoSecreto(comoPresenteador.RecebedorId, eventoId);

            return new PerguntarQuizResultadoDto
            {
                Pergunta = MapPerguntaAtiva(nova),
                NotificacaoRecebedor = notificacao
            };
        }

        public async Task<PerguntaAtivaDto> TrocarPerguntaQuiz(Guid perguntaAmigoSecretoId, Guid usuarioId, Guid novaPerguntaQuizId)
        {
            var atual = await _perguntaAmigoSecretoRepository.BuscarCompletaPorId(perguntaAmigoSecretoId)
                ?? throw new NotFoundException("Pergunta não encontrada.");

            if (atual.ResultadoSorteio.PresenteadorId != usuarioId)
                throw new UnathorizedException("Você só pode trocar suas próprias perguntas.");

            if (atual.Status != StatusPerguntaEnum.Ativa)
                throw new BusinessException("Esta pergunta não está mais ativa.");

            var novaPergunta = await _perguntaQuizRepository.BuscarComOpcoes(novaPerguntaQuizId);
            if (novaPergunta is null || !novaPergunta.Ativa)
                throw new NotFoundException("Pergunta não encontrada.");

            if (await _perguntaAmigoSecretoRepository.ExisteAtivaComPergunta(atual.ResultadoSorteioId, novaPerguntaQuizId))
                throw new BusinessException("Você já fez essa pergunta.");

            atual.Status = StatusPerguntaEnum.Substituida;
            await _perguntaAmigoSecretoRepository.CommitAsync();

            var nova = new PerguntaAmigoSecreto
            {
                ResultadoSorteioId = atual.ResultadoSorteioId,
                PerguntaQuizId = novaPerguntaQuizId,
                Status = StatusPerguntaEnum.Ativa,
                PerguntadaEm = DateTime.UtcNow
            };

            await _perguntaAmigoSecretoRepository.Inserir(nova);
            nova.PerguntaQuiz = novaPergunta;

            return MapPerguntaAtiva(nova);
        }

        public async Task<PerguntaRecebidaDto> ResponderQuiz(Guid perguntaAmigoSecretoId, Guid usuarioId, Guid opcaoId)
        {
            var pergunta = await _perguntaAmigoSecretoRepository.BuscarCompletaPorId(perguntaAmigoSecretoId)
                ?? throw new NotFoundException("Pergunta não encontrada.");

            if (pergunta.ResultadoSorteio.RecebedorId != usuarioId)
                throw new UnathorizedException("Você só pode responder perguntas destinadas a você.");

            if (pergunta.Status != StatusPerguntaEnum.Ativa)
                throw new BusinessException("Esta pergunta não está mais ativa.");

            var opcao = pergunta.PerguntaQuiz.Opcoes.FirstOrDefault(o => o.Id == opcaoId)
                ?? throw new BusinessException("A opção escolhida não pertence a esta pergunta.");

            pergunta.OpcaoRespostaId = opcao.Id;
            pergunta.RespondidaEm = DateTime.UtcNow;

            await _perguntaAmigoSecretoRepository.CommitAsync();

            return MapPerguntaRecebida(pergunta);
        }

        public async Task<List<PerguntaRecebidaDto>> ListarPerguntasRecebidas(Guid eventoId, Guid usuarioId)
        {
            var comoRecebedor = await _resultadoSorteioRepository.BuscarComoRecebedor(eventoId, usuarioId);
            if (comoRecebedor is null)
                return new List<PerguntaRecebidaDto>();

            var perguntas = await _perguntaAmigoSecretoRepository
                .ListarRecebidasPorEvento(eventoId, usuarioId);

            return perguntas.Select(MapPerguntaRecebida).ToList();
        }

        private PerguntaAtivaDto MapPerguntaAtiva(PerguntaAmigoSecreto p) => new()
        {
            PerguntaAmigoSecretoId = p.Id,
            PerguntaQuizId = p.PerguntaQuizId,
            Texto = p.PerguntaQuiz?.Texto ?? string.Empty,
            Resposta = p.OpcaoResposta is not null ? _mapper.Map<OpcaoQuizDto>(p.OpcaoResposta) : null,
            PerguntadaEm = p.PerguntadaEm,
            RespondidaEm = p.RespondidaEm
        };

        private PerguntaRecebidaDto MapPerguntaRecebida(PerguntaAmigoSecreto p) => new()
        {
            PerguntaAmigoSecretoId = p.Id,
            Texto = p.PerguntaQuiz?.Texto ?? string.Empty,
            Opcoes = _mapper.Map<List<OpcaoQuizDto>>(p.PerguntaQuiz?.Opcoes ?? new List<OpcaoQuiz>()),
            OpcaoRespostaId = p.OpcaoRespostaId
        };

        private static int? CalcularIdade(DateOnly nascimento)
        {
            if (nascimento == default)
                return null;

            var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
            var idade = hoje.Year - nascimento.Year;
            if (nascimento > hoje.AddYears(-idade))
                idade--;

            return idade;
        }

    }
}
