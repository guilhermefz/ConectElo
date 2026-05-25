using ConectElo.Domain.Areas.Geral.Enuns;

namespace ConectElo.Application.Areas.EventosArea.DTOs
{
    public class ConfirmacoesEventoDto
    {
        public StatusConfirmacaoEventoEnum? MinhaConfirmacao { get; set; }
        public List<ConfirmacaoMembroDto> Confirmacoes {  get; set; }
    }
}
