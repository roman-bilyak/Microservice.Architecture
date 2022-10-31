using MassTransit;

namespace Microservice.CQRS;

public interface IQueryHandler<TQuery> : IConsumer<TQuery>
    where TQuery : class, IQuery
{
}