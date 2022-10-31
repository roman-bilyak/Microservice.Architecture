namespace Microservice.CQRS;

public abstract class CreateCommand<TModel> : ICommand
    where TModel : notnull
{
    public TModel Model { get; protected set; }

    protected CreateCommand(TModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        Model = model;
    }
}