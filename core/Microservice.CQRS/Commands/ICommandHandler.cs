using MassTransit;

namespace Microservice.CQRS.Commands;

public interface ICommandHandler<TCommand> : IConsumer<TCommand>
    where TCommand : class, ICommand
{
}