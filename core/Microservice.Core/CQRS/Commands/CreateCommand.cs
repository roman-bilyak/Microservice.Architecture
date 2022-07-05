namespace Microservice.Core.CQRS.Commands
{
    public abstract class CreateCommand<TRequest, TResponse> : ICommand<TResponse>
        where TRequest : notnull
        where TResponse : notnull
    {
        public TRequest Model { get; init; }
    }
}
