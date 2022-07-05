using MediatR;

namespace Microservice.Application.CQRS.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<T> : IRequest<T>
        where T : notnull
    {
    }
}