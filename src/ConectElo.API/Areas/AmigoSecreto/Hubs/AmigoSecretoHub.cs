using ConectElo.Application.Areas.AmigoSecreto.InterfacesService;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ConectElo.API.Areas.AmigoSecreto.Hubs
{
    public class AmigoSecretoHub : Hub
    {
        private readonly IAmigoSecretoService _amigoSecretoService;

        public AmigoSecretoHub(IAmigoSecretoService amigoSecretoService)
        {
            _amigoSecretoService = amigoSecretoService;
        }

        public async Task EntrarChat(Guid resultadoSorteioId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                resultadoSorteioId.ToString());
        }

        public async Task EnviarMensagem(Guid resultadoSorteioId, string conteudo)
        {
            var usuarioId = Guid.Parse(Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var mensagem = await _amigoSecretoService.EnviarMensagem(resultadoSorteioId, usuarioId, conteudo);

            await Clients.Group(resultadoSorteioId.ToString()).SendAsync("ReceberMensagemAnonima", mensagem);
        }

        public async Task SairChat(Guid resultadoSorteioId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, resultadoSorteioId.ToString());
        }
    }
}
