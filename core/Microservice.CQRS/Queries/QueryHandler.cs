using MassTransit.Mediator;

namespace Microservice.Application;

public abstract class QueryHandler<TRequest, TResponse> : MediatorRequestHandler<TRequest, TResponse>, IQueryHandler<TRequest, TResponse>
    where TRequest : class, IQuery<TResponse>
    where TResponse : class
{
}