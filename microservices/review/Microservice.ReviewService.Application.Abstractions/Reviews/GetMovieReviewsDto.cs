using Microservice.Application;

namespace Microservice.ReviewService.Reviews;

public record GetMovieReviewsDto : ListDto<ReviewDto>
{
}