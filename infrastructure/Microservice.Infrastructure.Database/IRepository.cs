namespace Microservice.Infrastructure.Database;

public interface IRepository<TEntity> : IRepository<TEntity, Guid>, IReadRepository<TEntity>
    where TEntity : class, IAggregateRoot
{
}

public interface IRepository<TEntity, TKey> : IReadRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot
    where TKey : notnull
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}