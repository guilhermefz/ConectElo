using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IGrupoService
    {
        Task<CriarGrupoDto?> CriarGrupo(CriarGrupoDto grupo);
        Task ExcluirGrupo(Guid id);
        Task EditarGrupo(EditarGrupoDto grupo);
        Task<BuscarGrupoDto?> BuscarGrupoPorId(Guid id);
        Task<IEnumerable<GrupoExibicaoDto>> BuscarGruposPorUsuario(Guid usuarioId);
        Task<string> AtualizarFotoGrupoAsync(Guid grupoId, Guid usuarioId, AtualizarFotoDto foto);
        Task<ConviteGeradoDto> GerarCodigoConviteAsync(Guid grupoId, Guid usuarioId, TipoExpiracaoConviteEnum tipoExpiracao);
        Task<GrupoExibicaoDto> EntrarPorConviteAsync(string codigoConvite, Guid usuarioId);
    }
}
