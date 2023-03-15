namespace Microservice.Database;

public interface IReadRepository<TEntity> : IReadRepository<TEntity, Guid>
    where TEntity : class, IAggregateRoot
{
}

public interface IReadRepository<TEntity, TKey> 
    where TEntity : class, IAggregateRoot 
    where TKey : notnull
{
    Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);

    Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);

    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
}