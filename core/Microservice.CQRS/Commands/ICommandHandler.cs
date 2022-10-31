using MassTransit;

namespace Microservice.CQRS;

public interface ICommandHandler<TCommand> : IConsumer<TCommand>
    where TCommand : class, ICommand
{
}