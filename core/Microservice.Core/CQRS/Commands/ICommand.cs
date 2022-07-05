using MediatR;

namespace Microservice.Core.CQRS.Commands
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<T> : IRequest<T>
        where T : notnull
    {
    }
}