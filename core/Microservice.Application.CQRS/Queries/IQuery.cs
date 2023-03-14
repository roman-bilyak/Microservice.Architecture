using MassTransit.Mediator;

namespace Microservice.Application;

public interface IQuery<out TResponse> : Request<TResponse>
    where TResponse : class
{
}