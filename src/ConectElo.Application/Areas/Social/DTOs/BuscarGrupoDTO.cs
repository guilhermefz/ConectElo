using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.Social.DTOs
{
    public class BuscarGrupoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string? CodigoConvite { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataDelecao { get; set; }
        public string? ImgGrupo { get; set; }
        public bool Privado { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public Guid ProprietarioId { get; set; }
        public Guid MuralId { get; set; }

        public ICollection<MembroGrupoExibicaoDto> Membros { get; set; } = new List<MembroGrupoExibicaoDto>();
    }
}
