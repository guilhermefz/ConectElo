using ConectElo.Application.Areas.AmigoSecreto.Utils;
using ConectElo.Domain.Exceptions;

namespace ConectElo.Tests.Areas.AmigoSecreto.Utils
{
    public class SorteioAlgoritmoTests
    {
        [Fact]
        public void Sortear_ComMenosDeDoisParticipantes_BusinessException()
        {
            var participantes = new List<Guid> { Guid.NewGuid() };

            Action act = () => SorteioAlgoritmo.Sortear(participantes);

            var exception = Assert.Throws<BusinessException>(act);
            Assert.Equal("É necessário pelo menos 2 participantes para realizar o sorteio.", exception.Message);
        }

        [Fact]
        public void Sortear_ComParticipantesDuplicados_BusinessException()
        {
            var participantesId = new List<Guid> { Guid.NewGuid() };
            participantesId.Add(participantesId[0]);

            Action act = () => SorteioAlgoritmo.Sortear(participantesId);

            var exception = Assert.Throws<BusinessException>(act);
            Assert.Equal("Existem participantes duplicados na lista.", exception.Message);
        }

        [Fact]
        public void Sortear_ComParticipantesValidos_DeveRetornarPares()
        {
            var pessoa1 = Guid.NewGuid();
            var pessoa2 = Guid.NewGuid();
            var participantes = new List<Guid> { pessoa1, pessoa2 };

            var pares = SorteioAlgoritmo.Sortear(participantes);

            Assert.Equal(participantes.Count, pares.Count);
            Assert.All(pares, par =>
            {
                Assert.Contains(par.Presenteador, participantes);
                Assert.Contains(par.Recebedor, participantes);
            });
        }
    }
}
