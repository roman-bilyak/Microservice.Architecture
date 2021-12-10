using System.Linq.Expressions;

namespace Microservice.Infrastructure.Database;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T obj);

    Expression<Func<T, bool>> ToExpression();
}