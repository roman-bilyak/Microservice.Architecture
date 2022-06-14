using Microservice.Core.Database;

namespace Microservice.ReviewService.Reviews;

internal sealed class MovieReviewSpecification : Specification<Review>
{
    public MovieReviewSpecification(Guid movieId)
        : base(x => x.MovieId == movieId)
    {
    }
}