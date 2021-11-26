namespace Microservice.Infrastructure.Database;

public interface IReadRepository<T> : IReadRepository<T, Guid>
    where T : class, IAggregateRoot
{
}

public interface IReadRepository<T, TId> 
    where T : class, IAggregateRoot 
    where TId : notnull
{
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<T>> ListAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default(CancellationToken));

    Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default(CancellationToken));
}