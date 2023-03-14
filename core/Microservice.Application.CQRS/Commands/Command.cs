namespace Microservice.Application;

public abstract class Command<TResponse> : ICommand<TResponse>
    where TResponse : class
{
}