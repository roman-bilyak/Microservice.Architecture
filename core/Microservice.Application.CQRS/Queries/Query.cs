namespace Microservice.Application;

public abstract class Query<TResponse> : IQuery<TResponse>
    where TResponse : class
{
}