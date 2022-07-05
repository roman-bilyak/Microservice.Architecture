using MediatR;

namespace Microservice.Core.CQRS.Queries
{
    public interface IQuery<T> : IRequest<T>
        where T : notnull
    {
    }
}