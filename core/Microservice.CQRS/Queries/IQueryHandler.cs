using MassTransit;

namespace Microservice.CQRS;

public interface IQueryHandler<TQuery, TResponse> : IConsumer<TQuery>
    where TQuery : class, IQuery<TResponse>
    where TResponse : class
{
}