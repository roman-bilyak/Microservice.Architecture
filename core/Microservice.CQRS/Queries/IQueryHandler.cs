using MassTransit;

namespace Microservice.Application;

public interface IQueryHandler<TQuery, TResponse> : IConsumer<TQuery>
    where TQuery : class, IQuery<TResponse>
    where TResponse : class
{
}