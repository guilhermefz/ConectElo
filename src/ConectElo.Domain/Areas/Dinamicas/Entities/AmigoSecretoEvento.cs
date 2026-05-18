using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Eventos.Entities;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    public class AmigoSecretoEvento : Evento
    {
        public double Valor {  get; set; }
        public DateTime DataSorteio { get; set; }
        public string ResultadoSorteio { get; set; }
        public bool Sorteado { get; set;}
    }
}
