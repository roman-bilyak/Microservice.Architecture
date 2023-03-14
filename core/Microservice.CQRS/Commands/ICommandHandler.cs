using MassTransit;

namespace Microservice.Application;

public interface ICommandHandler<TCommand, TResponse> : IConsumer<TCommand>
    where TCommand : class, ICommand<TResponse>
    where TResponse : class
{
}