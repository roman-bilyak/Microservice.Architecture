using System.Linq.Expressions;

namespace Microservice.Database;

public abstract class Specification<T> : ISpecification<T>
{
    public Specification()
    {

    }

    public Specification(Expression<Func<T, bool>> criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria, nameof(criteria));

        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public List<string> IncludeStrings { get; } = new List<string>();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public Expression<Func<T, object>>? GroupBy { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; } = false;

    public bool IsTracking { get; private set; } = true;

    public ISpecification<T> AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);

        return this;
    }

    public ISpecification<T> AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);

        return this;
    }

    public ISpecification<T> ApplyPaging(int page, int size)
    {
        Skip = page * size;
        Take = size;
        IsPagingEnabled = true;

        return this;
    }

    public ISpecification<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;

        return this;
    }

    public ISpecification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;

        return this;
    }

    public ISpecification<T> ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
    {
        GroupBy = groupByExpression;

        return this;
    }

    public ISpecification<T> AsNoTracking()
    {
        IsTracking = false;

        return this;
    }
}