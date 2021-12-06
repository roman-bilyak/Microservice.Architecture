namespace Microservice.Infrastructure.Database;

public interface IReadRepository<TEntity> : IReadRepository<TEntity, Guid>
    where TEntity : class, IAggregateRoot
{
}

public interface IReadRepository<TEntity, TKey> 
    where TEntity : class, IAggregateRoot 
    where TKey : notnull
{
    Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default(CancellationToken));

    Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default(CancellationToken));
}