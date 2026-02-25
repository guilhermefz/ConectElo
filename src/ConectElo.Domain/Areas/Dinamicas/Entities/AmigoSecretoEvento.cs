using ConectElo.Domain.Areas.Base;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    public class AmigoSecretoEvento : EntityBase
    {
        public double Valor {  get; set; }
        public DateTime DataSorteio { get; set; }
        public string ResultadoSorteio { get; set; }
        public bool Sorteado { get; set;}
        public Guid EventoId { get; set; }
    }
}
