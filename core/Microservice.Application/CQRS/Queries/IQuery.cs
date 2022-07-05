using MediatR;

namespace Microservice.Application.CQRS.Queries
{
    public interface IQuery<T> : IRequest<T>
        where T : notnull
    {
    }
}