using Microservice.Infrastructure.Database;
using System.Linq.Expressions;

namespace Microservice.ReviewService.Reviews;

internal sealed class MovieReviewSpecification : Specification<Review>
{
    private readonly Guid _movieId;

    public MovieReviewSpecification(Guid movieId)
    {
        _movieId = movieId;
    }

    public override Expression<Func<Review, bool>> ToExpression()
    {
        return x => x.MovieId == _movieId;
    }
}
