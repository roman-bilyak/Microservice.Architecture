using MassTransit;

namespace Microservice.Application.CQRS.Commands
{
    public interface ICommandHandler<TCommand> : IConsumer<TCommand>
        where TCommand : class, ICommand
    {
    }
}