using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.Social.DTOs
{
    public class ConviteGeradoDto
    {
        public string Codigo { get; set; }
        public TipoExpiracaoConviteEnum TipoExpiracao { get; set; }
        public DateTime? ExpiraEm { get; set; }
    }
}
