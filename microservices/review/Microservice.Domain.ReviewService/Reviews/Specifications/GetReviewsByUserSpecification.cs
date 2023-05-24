using Microservice.Database;

namespace Microservice.ReviewService.Reviews;

internal class GetReviewsByUserSpecification : Specification<Review>
{
    public GetReviewsByUserSpecification(Guid userId)
        : base(x => x.UserId == userId)
    {
    }
}