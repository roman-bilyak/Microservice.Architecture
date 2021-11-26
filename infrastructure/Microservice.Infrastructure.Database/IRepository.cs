namespace Microservice.Infrastructure.Database;

public interface IRepository<T> : IReadRepository<T, Guid>
    where T : class, IAggregateRoot
{
}

public interface IRepository<T, TId> : IReadRepository<T, TId>
    where T : class, IAggregateRoot
    where TId : notnull
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
}