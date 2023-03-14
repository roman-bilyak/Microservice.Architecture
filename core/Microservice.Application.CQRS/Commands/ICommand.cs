using MassTransit.Mediator;

namespace Microservice.Application;

public interface ICommand<out TResponse> : Request<TResponse>
    where TResponse : class
{
}