namespace Microservice.Application.CQRS.Commands
{
    public abstract class UpdateCommand<TId, TModel> : ICommand
        where TId : struct
        where TModel : notnull
    {
        public TId Id { get; init; }

        public TModel Model { get; init; }
    }
}