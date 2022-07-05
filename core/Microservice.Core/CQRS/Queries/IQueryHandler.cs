using MediatR;

namespace Microservice.Core.CQRS.Queries
{
    public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}