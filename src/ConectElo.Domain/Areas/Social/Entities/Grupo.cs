using ConectElo.Domain.Areas.Base;

namespace ConectElo.Domain.Areas.Social.Entities
{
    public class Grupo : EntityBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string? CodigoConvite { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataDelecao { get; set; }
        public string? ImgGrupo { get; set; }
        public bool Privado { get; set;}
        public DateTime UltimaAtualizacao { get; set; } = DateTime.UtcNow;
        public Guid ProprietarioId { get; set;}
        public Guid MuralId { get; set; }

        public virtual ICollection<MembrosGrupo> Membros { get; set; } = new List<MembrosGrupo>();
    }
}
