using MassTransit.Mediator;

namespace Microservice.CQRS;

public interface IQuery<out TResponse> : Request<TResponse>
    where TResponse : class
{
}