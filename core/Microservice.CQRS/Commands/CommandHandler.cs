using MassTransit;
using MassTransit.Mediator;

namespace Microservice.CQRS;

public abstract class CommandHandler<TRequest> : CommandHandler<TRequest, Unit>
    where TRequest : class, ICommand<Unit>
{
}

public abstract class CommandHandler<TRequest, TResponse> : MediatorRequestHandler<TRequest, TResponse>, ICommandHandler<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>
    where TResponse : class
{
}