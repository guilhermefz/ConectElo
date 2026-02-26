namespace ConectElo.Domain.Areas.Base.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Consultar();
        Task<TEntity?> SelecionarPorId(Guid id);
        Task Inserir(TEntity entity);
        void Excluir(TEntity entity);
        void Atualizar(TEntity entity);
        Task<int> CommitAsync();
    }
}
