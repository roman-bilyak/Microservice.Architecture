namespace Microservice.Infrastructure.Database;

public interface ISpecification<T>
{
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
}