namespace Microservice.Application;

public abstract class UpdateCommand<TId, TModel, TResponse> : Command<TResponse>
    where TId : struct
    where TModel : notnull
    where TResponse : class
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