using System.Text.Json.Serialization;

namespace ConectElo.Application.Areas.Autenticacao.DTOs
{
    public class LoginDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
