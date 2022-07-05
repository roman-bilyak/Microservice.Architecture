namespace Microservice.Application.CQRS.Queries
{
    public abstract class ItemQuery<TId, TResponse> : IQuery<TResponse>
        where TId : struct
        where TResponse : notnull
    {
        public TId Id { get; init; }
    }
}