using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Dinamicas.Enuns;
using ConectElo.Domain.Areas.Eventos.Entities;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    public class AmigoSecretoEvento : Evento
    {
        public double Valor {  get; set; }
        public DateTime DataSorteio { get; set; }
        public DateTime? DataExecucaoSorteio { get; set; }
        public bool Sorteado { get; set;}
        public StatusSorteioEnum StatusSorteio { get; set; } = StatusSorteioEnum.AguardandoParticipantes;
        public virtual ICollection<ResultadoSorteio> Resultados { get; set; } = new List<ResultadoSorteio>();
    }
}
