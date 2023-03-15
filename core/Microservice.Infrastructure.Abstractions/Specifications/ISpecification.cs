using System.Linq.Expressions;

namespace Microservice.Database;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    Expression<Func<T, object>>? GroupBy { get; }

    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }

    bool IsTracking { get; }

    ISpecification<T> AddInclude(Expression<Func<T, object>> includeExpression);

    ISpecification<T> AddInclude(string includeString);

    ISpecification<T> ApplyPaging(int page, int size);

    ISpecification<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression);

    ISpecification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression);

    ISpecification<T> ApplyGroupBy(Expression<Func<T, object>> groupByExpression);

    ISpecification<T> AsNoTracking();
}