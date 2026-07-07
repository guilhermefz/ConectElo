using ConectElo.API.Areas.AmigoSecreto.Hubs;
using ConectElo.API.Areas.Base.Controllers;
using ConectElo.API.Areas.Comunicacao.Hubs;
using ConectElo.Application.Areas.AmigoSecreto.DTOs;
using ConectElo.Application.Areas.AmigoSecreto.InterfacesService;
using ConectElo.Application.Areas.Social.DTOs.EventosDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ConectElo.API.Areas.AmigoSecreto.Controllers
{
    [Authorize]
    [Route("api/AmigoSecreto")]
    [ApiController]
    public class AmigoSecretoController : BaseController
    {
        private readonly IAmigoSecretoService _amigoSecretoService;
        private readonly IHubContext<AmigoSecretoHub> _hubContext;
        private readonly IHubContext<ChatHub> _avisosHubContext;

        public AmigoSecretoController(IWebHostEnvironment env, IAmigoSecretoService amigoSecretoService, IHubContext<AmigoSecretoHub> hubContext, IHubContext<ChatHub> avisosHubContext) : base(env)
        {
            _amigoSecretoService = amigoSecretoService;
            _hubContext = hubContext;
            _avisosHubContext = avisosHubContext;
        }

        [HttpPost("{eventoId}/Agendar")]
        public async Task<IActionResult> AgendarSorteio(Guid eventoId, [FromBody] AgendarSorteioDto dto)
        {
            try
            {
                dto.EventoId = eventoId;
                var jobId = await _amigoSecretoService.AgendarSorteio(dto, UsuarioIdLogado);
                return OkResponse(new { jobId }, "Sorteio agendado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("{eventoId}/Sortear")]
        public async Task<IActionResult> Sortear(Guid eventoId)
        {
            try
            {
                var resultado = await _amigoSecretoService.Sortear(eventoId, UsuarioIdLogado);
                return OkResponse(resultado, "Sorteio realizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("{eventoId}/SortearAgora")]
        public async Task<IActionResult> SortearAgora(Guid eventoId) //obsoleto
        {
            try
            {
                var resultado = await _amigoSecretoService.SortearAgora(eventoId, UsuarioIdLogado);

                foreach (var participanteId in resultado.ParticipantesIds)
                {
                    await _hubContext.Clients
                        .User(participanteId.ToString())
                        .SendAsync("SorteioRealizado", new
                        {
                            resultado.EventoId,
                            resultado.DataExecucao
                        });
                }

                return OkResponse(resultado, "Sorteio realizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPut("{eventoId}/AlterarData")]
        public async Task<IActionResult> AlterarDataSorteio(
            Guid eventoId, [FromBody] AlterarDataSorteioDto dto)
        {
            try
            {
                var jobId = await _amigoSecretoService
                    .AlterarDataSorteio(eventoId, dto.NovaData, UsuarioIdLogado);

                return OkResponse(new { jobId }, "Data do sorteio alterada com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("{eventoId}/MeuResultado")]
        public async Task<IActionResult> BuscarMeuResultado(Guid eventoId)
        {
            try
            {
                var resultado = await _amigoSecretoService
                    .BuscarMeuResultado(eventoId, UsuarioIdLogado);

                return OkResponse(resultado, "Resultado buscado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("Chat/{resultadoSorteioId}/Historico")]
        public async Task<IActionResult> BuscarHistorico(Guid resultadoSorteioId)
        {
            try
            {
                var historico = await _amigoSecretoService
                    .BuscarHistorico(resultadoSorteioId, UsuarioIdLogado);

                return OkResponse(historico, "Histórico buscado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("{eventoId}/MinhaLista")]
        public async Task<IActionResult> BuscarMinhaLista(Guid eventoId)
        {
            try
            {
                var lista = await _amigoSecretoService.BuscarMinhaLista(eventoId, UsuarioIdLogado);
                return OkResponse(lista, "Lista buscada com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("{eventoId}/MinhaLista/Itens")]
        public async Task<IActionResult> AdicionarItemMinhaLista(Guid eventoId, [FromBody] CriarItemListaDesejosDto dto)
        {
            try
            {
                var item = await _amigoSecretoService.AdicionarItemMinhaLista(eventoId, UsuarioIdLogado, dto);
                return OkResponse(item, "Item adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpDelete("ListaDesejos/Itens/{itemId}")]
        public async Task<IActionResult> RemoverItemMinhaLista(Guid itemId)
        {
            try
            {
                await _amigoSecretoService.RemoverItemMinhaLista(itemId, UsuarioIdLogado);
                return OkResponse("Item removido com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("{eventoId}/Detalhe")]
        public async Task<IActionResult> BuscarDetalhe(Guid eventoId)
        {
            try
            {
                var detalhe = await _amigoSecretoService.BuscarDetalhe(eventoId, UsuarioIdLogado);
                return OkResponse(detalhe, "Detalhe buscado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("QuizPerguntas")]
        public async Task<IActionResult> ListarCatalogoQuiz()
        {
            try
            {
                var catalogo = await _amigoSecretoService.ListarCatalogoQuiz();
                return OkResponse(catalogo, "Perguntas buscadas com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("{eventoId}/Quiz/Perguntar")]
        public async Task<IActionResult> PerguntarQuiz(Guid eventoId, [FromBody] PerguntarQuizDto dto)
        {
            try
            {
                var resultado = await _amigoSecretoService
                    .PerguntarQuiz(eventoId, UsuarioIdLogado, dto.PerguntaQuizId);

                var aviso = resultado.NotificacaoRecebedor;
                await _avisosHubContext.Clients
                    .User(aviso.UsuarioId.ToString())
                    .SendAsync("ReceberAviso", aviso);

                return OkResponse(resultado.Pergunta, "Pergunta enviada com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPut("Quiz/{perguntaAmigoSecretoId}/Trocar")]
        public async Task<IActionResult> TrocarPerguntaQuiz(Guid perguntaAmigoSecretoId, [FromBody] TrocarPerguntaQuizDto dto)
        {
            try
            {
                var pergunta = await _amigoSecretoService
                    .TrocarPerguntaQuiz(perguntaAmigoSecretoId, UsuarioIdLogado, dto.NovaPerguntaQuizId);
                return OkResponse(pergunta, "Pergunta trocada com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPut("Quiz/{perguntaAmigoSecretoId}/Responder")]
        public async Task<IActionResult> ResponderQuiz(Guid perguntaAmigoSecretoId, [FromBody] ResponderQuizDto dto)
        {
            try
            {
                var pergunta = await _amigoSecretoService
                    .ResponderQuiz(perguntaAmigoSecretoId, UsuarioIdLogado, dto.OpcaoId);
                return OkResponse(pergunta, "Resposta registrada com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("{eventoId}/Quiz/Recebidas")]
        public async Task<IActionResult> ListarPerguntasRecebidas(Guid eventoId)
        {
            try
            {
                var perguntas = await _amigoSecretoService
                    .ListarPerguntasRecebidas(eventoId, UsuarioIdLogado);
                return OkResponse(perguntas, "Perguntas buscadas com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    }
}
