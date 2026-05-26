using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using ConectElo.Application.Areas.Social.DTOs.EventosDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConectElo.API.Areas.Eventos
{
    [Authorize]
    [Route("api/Eventos")]
    [ApiController]
    public class EventosController : BaseController
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService, IWebHostEnvironment env) : base(env) 
        {
            _eventoService = eventoService;
        }

        [HttpPost("Aniversario")]
        public async Task<IActionResult> CriarAniversario(CriarAniversarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequestResponse("Os dados não são válidos para a criação.");

                var criadorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var evento = await _eventoService.CriarAniversario(dto, criadorId);
                return OkResponse(evento, "Evento criado com sucesso!");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("AmigoSecreto")]
        public async Task<IActionResult> CriarAmigoSecreto(CriarAmigoSecretoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequestResponse("Os dados não são válidos para criação");

                var criadorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var evento = await _eventoService.CriarAmigoSecreto(dto, criadorId);
                return OkResponse(evento, "Evento criado com sucesso!");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("ListarPorGrupo/{grupoId}")]
        public async Task<IActionResult> ListarPorGrupo(Guid grupoId)
        {
            try
            {
                if (grupoId == Guid.Empty)
                    return BadRequestResponse("O Id do grupo não é válido.");

                var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var eventos = await _eventoService.ListarPorGrupo(grupoId, usuarioId);
                return OkResponse(eventos, "Eventos listados com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("Salvar")]
        public async Task<IActionResult> CriarEvento (CriarEventoDto evento)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequestResponse("Os dados não são válidos para criação.");

                await _eventoService.CriarEvento(evento);
                return OkResponse("Evento criado com sucesso!");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpDelete("Deletar")]
        public async Task<IActionResult> Excluir (Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return NotFoundResponse("O Id enviado não é válido.");

                await _eventoService.ExcluirEvento(id);
                return OkResponse("Evento deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("BuscarPorId")]
        public async Task<IActionResult> BuscarPorId (Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return NotFoundResponse("O Id enviado não é válido.");

                var evento = await _eventoService.BuscarEventoPorId(id);
                return OkResponse<object>(evento, "Evento buscado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> EditarEvento(EditarEventoDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequestResponse("os dados não são válidos para edição.");

                var evento = await _eventoService.EditarEvento(model);
                return OkResponse(evento, "evento editado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("ListarDoUsuario")]
        public async Task<IActionResult> ListarDoUsuario()
        {
            try
            {
                var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var eventos = await _eventoService.ListarPorUsuario(usuarioId);
                return OkResponse(eventos, "Eventos listados com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("FotoCapa/{eventoId}")]
        public async Task<IActionResult> AtualizarFotoCapa(Guid eventoId, IFormFile foto)
        {
            try
            {
                if (foto == null || foto.Length == 0)
                    return BadRequest("Nenhuma foto enviada");

                await using var stream = foto.OpenReadStream();
                var url = await _eventoService.AtualizarFotoCapa(eventoId, stream, foto.FileName, foto.Length);

                return OkResponse(url, "Foto de capa atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("{eventoId}/Participacao")]
        public async Task<IActionResult> RegistrarParticipacao(Guid eventoId, RegistrarParticipacaoDto dto)
        {
            try
            {
                var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                await _eventoService.RegistrarParticipacao(eventoId, usuarioId, dto.Status);
                return OkResponse("Participação registrada com sucesso.");
            }
            catch (Exception ex) { return ErrorResponse(ex); }
        }

        [HttpPut("ListaDesejos/Selecionar/{itemId}")]
        public async Task<IActionResult> SelecionarItem(Guid itemId)
        {
            try
            {
                var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var item = await _eventoService.SelecionarItem(itemId, usuarioId);
                return OkResponse(item, "Item selecionado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpDelete("ListaDesejos/Selecionar/{itemId}")]
        public async Task<IActionResult> DeselecionarItem(Guid itemId)
        {
            try
            {
                var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var item = await _eventoService.DeselecionarItem(itemId, usuarioId);
                return OkResponse(item, "Item liberado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("ListaDesejos/{listaId}/Itens")]
        public async Task<IActionResult> AdicionarItem(Guid listaId, [FromBody] CriarItemListaDesejosDto dto)
        {
            try
            {
                var criadorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var item = await _eventoService.AdicionarItemListaDesejos(listaId, dto, criadorId);
                return OkResponse(item, "Item adicionado com sucesso.");
            }
            catch (Exception ex) { return ErrorResponse(ex); }
        }

        [HttpDelete("ListaDesejos/Itens/{itemId}")]
        public async Task<IActionResult> RemoverItem(Guid itemId)
        {
            try
            {
                var criadorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                await _eventoService.RemoverItemListaDesejos(itemId, criadorId);
                return OkResponse("Item removido com sucesso.");
            }
            catch (Exception ex) { return ErrorResponse(ex); }
        }
    }
}
