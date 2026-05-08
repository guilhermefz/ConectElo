using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.Enuns;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Domain.Exceptions;
using ConectElo.Application.Areas.Social.Utils;

namespace ConectElo.Application.Areas.Social.Services
{
    public class GrupoService : IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMuralRepository _muralRepository;
        private readonly IMembrosGrupoRepository _membrosGrupoRepository;
        private readonly IArquivoService _arquivoService;
        private readonly IMapper _mapper;

        public GrupoService(IGrupoRepository grupoRepository, IMapper mapper, IMuralRepository muralRepository, IArquivoService arquivoService, IMembrosGrupoRepository membrosGrupoRepository)
        {
            _grupoRepository = grupoRepository;
            _mapper = mapper;
            _muralRepository = muralRepository;
            _arquivoService = arquivoService;
            _membrosGrupoRepository = membrosGrupoRepository;
        }

        public async Task<BuscarGrupoDto?> BuscarGrupoPorId(Guid id)
        {
            var grupo = await _grupoRepository.ObterGrupoComInclude(id);
            return _mapper.Map<BuscarGrupoDto>(grupo);

        }

        public async Task<CriarGrupoDto?> CriarGrupo(CriarGrupoDto dto)
        {
            var mural = new Mural();
            await _muralRepository.Inserir(mural);

            var grupo = _mapper.Map<Grupo>(dto);

            grupo.MuralId = mural.Id;
            var agora = DateTime.UtcNow;
            grupo.DataCriacao = agora;
            grupo.UltimaAtualizacao = agora;
            grupo.Membros.Add(new MembrosGrupo
            {
                UsuarioId = dto.ProprietarioId,
                DataEntrada = DateTime.UtcNow,
                Tipo = TipoPermissaoMembroEnum.Proprietario,
            });

            await _grupoRepository.Inserir(grupo);

            var grupoCriado = await _grupoRepository.ObterGrupoComInclude(grupo.Id);
            return _mapper.Map<CriarGrupoDto>(grupoCriado);
        }

        public async Task EditarGrupo(EditarGrupoDto dto)
        {
            var grupoSemEdicao = await _grupoRepository.SelecionarPorId(dto.id);

            if (grupoSemEdicao == null)
                throw new Exception("Grupo não encontrado");


            var grupo = _mapper.Map(dto, grupoSemEdicao);

            await _grupoRepository.Atualizar(grupo);
        }

        public async Task<IEnumerable<GrupoExibicaoDto>> BuscarGruposPorUsuario(Guid usuarioId)
        {
            var grupos = await _grupoRepository.BuscarPorUsuario(usuarioId);
            return _mapper.Map<IEnumerable<GrupoExibicaoDto>>(grupos);
        }

        public async Task ExcluirGrupo(Guid id)
        {
            var grupo = await _grupoRepository.SelecionarPorId(id);
            await _grupoRepository.Excluir(grupo);
        }

        public async Task<string> AtualizarFotoGrupoAsync(Guid grupoId, Guid usuarioId, AtualizarFotoDto foto)
        {
            var grupo = await _grupoRepository.ObterGrupoComInclude(grupoId);

            if (grupo is null)
                throw new NotFoundException("Grupo não encontrado.");

            var membro = grupo.Membros.FirstOrDefault(m => m.UsuarioId == usuarioId);
            if (membro is null || membro.Tipo == TipoPermissaoMembroEnum.Comum)
                throw new UnathorizedException("Apenas administradores e proprietários podem alterar a foto do grupo.");

            if (!string.IsNullOrEmpty(grupo.ImgGrupo))
                _arquivoService.DeletarArquivo(grupo.ImgGrupo);

            var urlNovaFoto = await _arquivoService.SalvarFotoGrupoAsync(foto, grupoId);

            grupo.ImgGrupo = urlNovaFoto;
            grupo.UltimaAtualizacao = DateTime.UtcNow;

            await _grupoRepository.Atualizar(grupo);

            return urlNovaFoto;
        }

        public async Task<ConviteGeradoDto> GerarCodigoConviteAsync(Guid grupoId, Guid usuarioId, TipoExpiracaoConviteEnum tipoExpiracao)
        {
            var grupo = await _grupoRepository.ObterGrupoComInclude(grupoId);

            if (grupo is null)
                throw new NotFoundException("Grupo não encontrado.");

            var membro = grupo.Membros.FirstOrDefault(m => m.UsuarioId == usuarioId);
            if (membro is null || membro.Tipo == TipoPermissaoMembroEnum.Comum)
                throw new UnathorizedException("Apenas administradores e proprietários podem gerar o link de convite.");

            var codigo = ConviteUtils.GerarCodigo();
            var expiracao = ConviteUtils.CalcularExpiracao(tipoExpiracao);

            grupo.CodigoConvite = codigo;
            grupo.CodigoConviteExpiracao = expiracao;
            grupo.TipoExpiracaoEConvite = tipoExpiracao;
            grupo.UltimaAtualizacao = DateTime.UtcNow;

            await _grupoRepository.Atualizar(grupo);

            return new ConviteGeradoDto
            {
                Codigo = codigo,
                TipoExpiracao = tipoExpiracao,
                ExpiraEm = expiracao
            };
        }

        public async Task<GrupoExibicaoDto> EntrarPorConviteAsync(string codigoConvite, Guid usuarioId)
        {
            var grupo = await _grupoRepository.BuscarPorCodigoConvite(codigoConvite);

            if (grupo is null)
                throw new NotFoundException("Convite inválido ou expirado.");

            if (grupo.CodigoConviteExpiracao.HasValue && grupo.CodigoConviteExpiracao < DateTime.UtcNow)
                throw new NotFoundException("Convite inválido ou expirado.");

            var jaEMembro = grupo.Membros.Any(m => m.UsuarioId == usuarioId);
            if (jaEMembro)
                throw new ConflictException("Você já é membro deste grupo.");

            await _membrosGrupoRepository.Inserir(new MembrosGrupo
            {
                UsuarioId = usuarioId,
                GrupoId = grupo.Id,
                Tipo = TipoPermissaoMembroEnum.Comum,
                DataEntrada = DateTime.UtcNow,
            });

            return _mapper.Map<GrupoExibicaoDto>(grupo);
        }
    }
}
