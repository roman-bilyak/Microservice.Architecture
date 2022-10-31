namespace Microservice.CQRS.Commands;

public abstract class CreateCommand<TModel> : ICommand
    where TModel : notnull
{
    public TModel Model { get; init; }
}