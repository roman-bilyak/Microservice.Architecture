namespace Microservice.CQRS;

public abstract class Query<TResponse> : IQuery<TResponse>
    where TResponse : class
{
}