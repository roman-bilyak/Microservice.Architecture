using Microsoft.EntityFrameworkCore;

namespace Microservice.Database;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(this IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        where TEntity : class
    {
        var query = inputQuery;

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy is not null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip)
                         .Take(specification.Take);
        }

        return query;
    }
}