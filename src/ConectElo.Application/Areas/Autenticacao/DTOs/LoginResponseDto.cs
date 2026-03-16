namespace ConectElo.Application.Areas.Autenticacao.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
