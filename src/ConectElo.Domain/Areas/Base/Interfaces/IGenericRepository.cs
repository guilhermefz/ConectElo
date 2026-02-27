namespace ConectElo.Domain.Areas.Base.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Consultar();
        Task<TEntity?> SelecionarPorId(Guid id);
        Task Inserir(TEntity entity);
        Task Excluir(TEntity entity);
        Task Atualizar(TEntity entity);
        Task<int> CommitAsync();
    }
}
