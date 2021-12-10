using Microservice.Core.Services;

namespace Microservice.ReviewService.Reviews;

internal class UserApplicationService : ApplicationService, IUserApplicationService
{
    private readonly IReviewManager _reviewManager;

    public UserApplicationService(IReviewManager reviewManager)
    {
        _reviewManager = reviewManager;
    }

    public async Task<GetUserReviewsDto> GetUserReviewsAsync(Guid id, CancellationToken cancellationToken)
    {
        GetUserReviewsDto result = new GetUserReviewsDto();
        foreach (Review review in await _reviewManager.GetListByUserAsync(id, cancellationToken))
        {
            result.Add(new ReviewDto
            {
                Id = review.Id,
                UserId = review.UserId,
                MovieId = review.MovieId,
                Text = review.Text,
                Rating = review.Rating
            });
        }
        return result;
    }
}