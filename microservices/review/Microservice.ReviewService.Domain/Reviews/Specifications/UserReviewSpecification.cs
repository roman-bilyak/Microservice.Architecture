using Microservice.Core.Database;

namespace Microservice.ReviewService.Reviews;

internal class UserReviewSpecification : Specification<Review>
{
    public UserReviewSpecification(Guid userId)
        : base(x => x.UserId == userId)
    {
    }
}