namespace Microservice.CQRS.Commands;

public abstract class DeleteCommand<TId> : ICommand
    where TId : struct
{
    public TId Id { get; init; }
}