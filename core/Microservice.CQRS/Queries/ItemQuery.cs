namespace Microservice.CQRS;

public abstract class ItemQuery<TId> : IQuery
    where TId : struct
{
    public TId Id { get; protected set; }

    protected ItemQuery(TId id)
    {
        Id = id;
    }
}