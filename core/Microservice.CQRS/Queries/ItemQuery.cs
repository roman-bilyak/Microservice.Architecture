namespace Microservice.CQRS;

public abstract class ItemQuery<TId, TResponse> : Query<TResponse>
    where TId : struct
    where TResponse : class
{
    public TId Id { get; protected set; }

    protected ItemQuery(TId id)
    {
        Id = id;
    }
}