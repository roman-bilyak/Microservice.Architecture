namespace Microservice.CQRS;

public abstract class Command<TResponse> : ICommand<TResponse>
    where TResponse : class
{
}