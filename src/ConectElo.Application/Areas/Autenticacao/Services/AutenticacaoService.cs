using ConectElo.Application.Areas.Autenticacao.DTOs;
using ConectElo.Application.Areas.Autenticacao.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConectElo.Application.Areas.Autenticacao.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;

        public AutenticacaoService(UserManager<Usuario> userManager, IConfiguration configuration) 
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> Login(LoginDto dto)
        {
            var usuario = await _userManager.FindByEmailAsync(dto.Email)
                ?? throw new UnathorizedException("E-mail ou senha inválidos.");

            var senhaValida = await _userManager.CheckPasswordAsync(usuario, dto.Password);
            if (!senhaValida)
                throw new UnathorizedException("E-mail ou senha inválidos.");

            return new LoginResponseDto
            {
                AccessToken = GerarToken(usuario),
                ExpiresIn = int.Parse(_configuration["Jwt:ExpiresInHours"]!) * 3600
            };
        }

        private string GerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiracao = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:ExpiresInHours"]!));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email!),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiracao,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
