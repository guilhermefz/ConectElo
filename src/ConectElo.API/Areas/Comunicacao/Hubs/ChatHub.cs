using ConectElo.Application.Areas.Comunicacao.InterfacesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConectElo.API.Areas.Comunicacao.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMensagemService _mensageService;

        public ChatHub(IMensagemService mensageService)
        {
            _mensageService = mensageService;
        }

        public async Task EntrarNoGrupo(string grupoId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, grupoId);
        }

        public async Task EnviarMensagem(string grupoId, string conteudo)
        {
            var usuarioId = Guid.Parse(Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var nomeAutor = Context.User!.FindFirstValue(JwtRegisteredClaimNames.Name) ?? "";
            var mensagem = await _mensageService.EnviarMensagemAsync(Guid.Parse(grupoId), usuarioId, conteudo);

            await Clients.Group(grupoId).SendAsync("ReceberMensagem", new
            {
                mensagem.Id,
                mensagem.Conteudo,
                NomeAutor = nomeAutor,
                UsuarioId = usuarioId,
                mensagem.HorarioEnvio
            });
        }
    }
}
