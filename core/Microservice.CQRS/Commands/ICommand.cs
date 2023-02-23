using MassTransit.Mediator;

namespace Microservice.CQRS;

public interface ICommand<out TResponse> : Request<TResponse>
    where TResponse : class
{
}