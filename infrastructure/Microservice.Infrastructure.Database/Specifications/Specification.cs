using System.Linq.Expressions;

namespace Microservice.Infrastructure.Database;
public abstract class Specification<T> : ISpecification<T>
{
    public bool IsSatisfiedBy(T obj)
    {
        return ToExpression().Compile()(obj);
    }

    public abstract Expression<Func<T, bool>> ToExpression();
}
