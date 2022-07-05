namespace Microservice.Core.CQRS.Commands
{
    public abstract class DeleteCommand<TId> : ICommand
        where TId : struct
    {
        public TId Id { get; init; }
    }
}