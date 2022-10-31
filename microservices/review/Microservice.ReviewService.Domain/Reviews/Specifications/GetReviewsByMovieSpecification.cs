using Microservice.Database;

namespace Microservice.ReviewService.Reviews;

internal sealed class GetReviewsByMovieSpecification : Specification<Review>
{
    public GetReviewsByMovieSpecification(Guid movieId)
        : base(x => x.MovieId == movieId)
    {
    }
}