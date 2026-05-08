using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ConectElo.Application.Areas.Social.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IArquivoService _arquivoService;
        private readonly IMapper _mapper;

        public UsuarioService(UserManager<Usuario> userManager, IMapper mapper, IArquivoService arquivoService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _arquivoService = arquivoService;
        }

        public async Task<IdentityResult> CriarUsuario(RegistrarUsuarioDto usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);
            return await _userManager.CreateAsync(user, usuario.password);
        }

        public async Task<IdentityResult> ExcluirUsuario(Usuario usuario)
        {
            return await _userManager.DeleteAsync(usuario);
        }

        public async Task<IdentityResult> EditarUsuario(EditarUsuarioDto dto)
        {
            var usuario = await _userManager.FindByIdAsync(dto.Id.ToString())
                ?? throw new NotFoundException($"Usuário com ID {dto.Id} não foi encontrado.");

            _mapper.Map(dto, usuario);

            return await _userManager.UpdateAsync(usuario);
        }

        public async Task<Usuario?> BuscarUsuarioPorId(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<PerfilUsuarioDto> ObterPerfilAsync(Guid usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId.ToString());

            if (usuario == null)
                throw new NotFoundException("Usuario não encontrado.");

            return _mapper.Map<PerfilUsuarioDto>(usuario);
        }

        public async Task<PerfilUsuarioDto> AtualizarPerfilAsync(Guid usuarioId, AtualizarPerfilDto dto)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId.ToString());

            if (usuario == null)
                throw new NotFoundException("Usuario não encontrado");

            if(!string.Equals(usuario.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailEmUso = await _userManager.FindByEmailAsync(dto.Email);
                if (emailEmUso is not null)
                    throw new ConflictException("Este e-mail já está em uso");
            }

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.UserName = dto.Email;
            usuario.Bio = dto.Bio;
            usuario.DataNascimento = dto.DataNascimento;
            usuario.Genero = dto.Genero;
            usuario.UltimaAtualizacao = DateTime.UtcNow;

            var resultado = await _userManager.UpdateAsync(usuario);

            if (!resultado.Succeeded)
                throw new BusinessException("Erro ao atualizar perfil.");

            return _mapper.Map<PerfilUsuarioDto>(usuario);
        }

        public async Task<string> AtualizarFotoPerfilAsync(Guid usuarioId, AtualizarFotoDto foto)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId.ToString());

            if (usuario is null)
                throw new NotFoundException("Usuário não encontrado");

            if (!string.IsNullOrEmpty(usuario.FotoPerdilUrl))
                _arquivoService.DeletarArquivo(usuario.FotoPerdilUrl);

            var urlNovaFoto = await _arquivoService.SalvarFotoPerfilAsync(foto, usuarioId);

            usuario.FotoPerdilUrl = urlNovaFoto;
            usuario.UltimaAtualizacao = DateTime.UtcNow;

            var resultado = await _userManager.UpdateAsync(usuario);

            if (!resultado.Succeeded)
                throw new NotFoundException("Erro ao salvar foto de perfil");

            return urlNovaFoto;
        }
    }
}
