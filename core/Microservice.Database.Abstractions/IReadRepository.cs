namespace Microservice.Database;

public interface IReadRepository<TEntity> : IReadRepository<TEntity, Guid>
    where TEntity : class, IAggregateRoot
{
}

public interface IReadRepository<TEntity, TKey> 
    where TEntity : class, IAggregateRoot 
    where TKey : notnull
{
    Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken);

    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken);

    Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);

    Task<int> CountAsync(CancellationToken cancellationToken);

    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
}