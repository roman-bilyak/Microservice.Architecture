namespace Microservice.Application.CQRS.Queries
{
    public abstract class ItemQuery<TId> : IQuery
        where TId : struct
    {
        public TId Id { get; init; }
    }
}