namespace Microservice.Core.CQRS.Commands
{
    public abstract class UpdateCommand<TId, TRequest, TResponse> : ICommand<TResponse>
        where TId : struct
        where TRequest : notnull
        where TResponse : notnull
    {
        public TId Id { get; init; }

        public TRequest Model { get; init; }
    }
}