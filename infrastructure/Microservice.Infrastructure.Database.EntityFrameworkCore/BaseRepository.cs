using Microsoft.EntityFrameworkCore;

namespace Microservice.Infrastructure.Database.EntityFrameworkCore;

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

    public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().Where(specification.ToExpression()).ToListAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().CountAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().Where(specification.ToExpression()).CountAsync(cancellationToken);
    }

    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Add(entity);

        return Task.FromResult(entity);
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;

        return Task.FromResult(entity);
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Remove(entity);

        return Task.CompletedTask;
    }

    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}