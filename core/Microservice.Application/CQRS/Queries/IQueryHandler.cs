using MassTransit;

namespace Microservice.Application.CQRS.Queries
{
    public interface IQueryHandler<TQuery> : IConsumer<TQuery>
        where TQuery : class, IQuery
    {
    }
}