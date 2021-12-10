using Microservice.Infrastructure.Database;
using System.Linq.Expressions;

namespace Microservice.ReviewService.Reviews;

internal class UserReviewSpecification : Specification<Review>
{
    private readonly Guid _userId;

    public UserReviewSpecification(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<Review, bool>> ToExpression()
    {
        return x => x.UserId == _userId;
    }
}
