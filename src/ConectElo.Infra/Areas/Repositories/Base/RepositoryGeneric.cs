using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Repositories.Base
{
    public abstract class RepositoryGeneric<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected RepositoryGeneric(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
           _dbSet.Update(entity);
           await CommitAsync();
        }

        public virtual async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> Consultar()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual async Task Excluir(TEntity entity)
        {
            _dbSet.Remove(entity);
            await CommitAsync();
        }

        public virtual async Task Inserir(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await CommitAsync();
        }

        public virtual async Task<TEntity?> SelecionarPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
