using MassTransit;

namespace Microservice.CQRS.Queries;

public interface IQueryHandler<TQuery> : IConsumer<TQuery>
    where TQuery : class, IQuery
{
}