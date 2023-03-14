namespace Microservice.Application;

public abstract class CreateCommand<TModel, TResponse> : Command<TResponse>
    where TModel : notnull
    where TResponse : class
{
    public TModel Model { get; protected set; }

    protected CreateCommand(TModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        Model = model;
    }
}