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
    }
}
