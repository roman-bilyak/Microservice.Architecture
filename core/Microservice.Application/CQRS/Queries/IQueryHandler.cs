using MediatR;

namespace Microservice.Application.CQRS.Queries
{
    public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}