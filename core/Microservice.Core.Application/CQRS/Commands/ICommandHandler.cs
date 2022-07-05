using MediatR;

namespace Microservice.Application.CQRS.Commands
{
    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : ICommand
    {
    }

    public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}