using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Domain.Areas.Geral.Entities;
using ConectElo.Domain.Areas.Geral.Enuns;

namespace ConectElo.Application.Areas.EventosArea.Services
{
    public class ConfirmacaoEventoService : IConfirmacaoEventoService
    {
        private readonly IConfirmacaoEventoRepository _confirmacaoEventoRepository;

        public ConfirmacaoEventoService(IConfirmacaoEventoRepository confirmacaoEventoRepository)
        {
            _confirmacaoEventoRepository = confirmacaoEventoRepository;
        }

        public async Task<ConfirmacoesEventoDto> ListarConfirmacoes(Guid eventoId, Guid usuarioId)
        {
            var lista = await _confirmacaoEventoRepository.ListarPorEvento(eventoId);

            return new ConfirmacoesEventoDto
            {
                MinhaConfirmacao = lista.FirstOrDefault(c => c.UsuarioId == usuarioId)?.Status,
                Confirmacoes = lista
                .Where(c => c.UsuarioId != usuarioId)
                .Select(c => new ConfirmacaoMembroDto
                {
                    UsuarioId = c.UsuarioId,
                    Nome = c.Usuario!.Nome,
                    FotoPerfil = c.Usuario.FotoPerdilUrl,
                    Status = c.Status
                }).ToList()
            };
        }

        public async Task Registrar(Guid eventoId, Guid usuarioId, StatusConfirmacaoEventoEnum status)
        {
            var existente = await _confirmacaoEventoRepository.BuscarPorEventoEUsuario(eventoId, usuarioId);

            if (existente is not null)
            {
                existente.Status = status;
                existente.DataAtualizacao = DateTime.UtcNow;
                await _confirmacaoEventoRepository.Atualizar(existente);
            }
            else
            {
                await _confirmacaoEventoRepository.Inserir(new ConfirmacaoEvento
                {
                    EventoId = eventoId,
                    UsuarioId = usuarioId,
                    Status = status,
                    DataAtualizacao = DateTime.UtcNow
                });
            }
        }
    }
}
