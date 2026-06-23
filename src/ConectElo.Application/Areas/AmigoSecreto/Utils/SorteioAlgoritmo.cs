using ConectElo.Domain.Exceptions;

namespace ConectElo.Application.Areas.AmigoSecreto.Utils
{
    public static class SorteioAlgoritmo
    {
        public static List<(Guid Presenteador, Guid Recebedor)> Sortear(List<Guid> participantes)
        {
            if (participantes.Count < 2)
                throw new BusinessException("É necessário pelo menos 2 participantes para realizar o sorteio.");

            if (participantes.Distinct().Count() != participantes.Count)
                throw new BusinessException("Existem participantes duplicados na lista.");

            var recebedores = participantes.ToList();
            var ramdom = new Random();

            for (int i = recebedores.Count -1; i > 0; i--)
            {
                int j = ramdom.Next(0, i);
                (recebedores[i], recebedores[j]) = (recebedores[j], recebedores[i]);
            }

            return participantes.Zip(recebedores, (presenteador, recebedor ) => (presenteador, recebedor)).ToList();
        }
    }
}
