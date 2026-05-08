using ConectElo.Domain.Areas.Social.Enuns;
using System.Security.Cryptography;

namespace ConectElo.Application.Areas.Social.Utils
{
    public static class ConviteUtils
    {
        private static readonly char[] _alfabeto = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ".ToCharArray();

        public static string GerarCodigo()
        {
            var bytes = RandomNumberGenerator.GetBytes(8);
            return new string(bytes.Select(b => _alfabeto[b % _alfabeto.Length]).ToArray());
        }

        public static DateTime? CalcularExpiracao(TipoExpiracaoConviteEnum tipo) => tipo switch
        {
            TipoExpiracaoConviteEnum.QuinzeMinutos => DateTime.UtcNow.AddMinutes(15),
            TipoExpiracaoConviteEnum.UmaHora => DateTime.UtcNow.AddHours(1),
            TipoExpiracaoConviteEnum.OitoHoras => DateTime.UtcNow.AddHours(8),
            TipoExpiracaoConviteEnum.UmDia => DateTime.UtcNow.AddDays(1),
            TipoExpiracaoConviteEnum.SeteDias => DateTime.UtcNow.AddDays(7),
            TipoExpiracaoConviteEnum.SemExpiracao => null,
            _ => throw new ArgumentOutOfRangeException(nameof(tipo))
        };
    }
}
