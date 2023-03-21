using Management.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Managemrnt.EFCore
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public Repository(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public DbSet<TEntity> DbSet => Context.Set<TEntity>();


        public async Task<TEntity> InsertAsync(
            TEntity entity,
            bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            if (autoSave)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(
            TEntity entity, 
            bool autoSave = false, 
            CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);
            if (autoSave)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }

        public async Task DeleteAsync(
            TEntity entity, 
            bool autoSave = false, 
            CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);
            if (autoSave)
            {
               await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(predicate).CountAsync(cancellationToken);
        }
    }

    public class Repository<TKey, TEntity>
        : Repository<TEntity>, IRepository<TKey, TEntity> where TEntity : Entity<TKey>
    {
        public Repository(AppDbContext context) : base(context)
        {
        }

        public async Task DeleteAsync(
            TKey id, 
            bool autoSave = false, 
            CancellationToken cancellationToken = default)
        {
            TEntity? entity = await DbSet.FindAsync(new object[] { id! }, cancellationToken);
            if (entity != null)
            {
                await DeleteAsync(entity, false, cancellationToken);
            }
        }

        public async Task<TEntity?> GetAsync(
            TKey id, 
            CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(new object[] { id! }, cancellationToken);
        }
    }
}
