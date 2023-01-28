using Microservice.Application;

namespace Microservice.ReviewService.Reviews;

public record GetUserReviewsDto : ListDto<ReviewDto>
{
}