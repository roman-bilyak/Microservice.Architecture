namespace Microservice.CQRS;

public abstract class UpdateCommand<TId, TModel> : Command
    where TId : struct
    where TModel : notnull
{
    public TId Id { get; protected set; }

    public TModel Model { get; protected set; }

    protected UpdateCommand(TId id, TModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        Id = id;
        Model = model;
    }
}