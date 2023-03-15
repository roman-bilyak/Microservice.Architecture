using Microsoft.EntityFrameworkCore;

namespace Microservice.Database;

public class BaseRepository<TDbContext, TEntity> : BaseRepository<TDbContext, TEntity, Guid>, IRepository<TEntity>
    where TDbContext : BaseDbContext<TDbContext>
    where TEntity : class, IAggregateRoot
{
    public BaseRepository(TDbContext dbContext) : base(dbContext)
    {
    }
}

public class BaseRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TDbContext : BaseDbContext<TDbContext>
    where TEntity : class, IAggregateRoot
    where TKey : notnull
{
    private readonly TDbContext _dbContext;

    public BaseRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().ApplySpecification(specification).ToListAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().ApplySpecification(specification).CountAsync(cancellationToken);
    }

    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().Add(entity);

        return Task.FromResult(entity);
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;

        return Task.FromResult(entity);
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().Remove(entity);

        return Task.CompletedTask;
    }

    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}