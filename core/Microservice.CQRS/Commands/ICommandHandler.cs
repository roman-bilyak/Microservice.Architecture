using MassTransit;

namespace Microservice.CQRS;

public interface ICommandHandler<TCommand, TResponse> : IConsumer<TCommand>
    where TCommand : class, ICommand<TResponse>
    where TResponse : class
{
}