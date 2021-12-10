using Microservice.Core.Services;
using Microservice.ReviewService.Domain.Reviews;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Application.Reviews;

internal class MovieApplicationService : ApplicationService, IMovieApplicationService
{
    private readonly IReviewManager _reviewManager;

    public MovieApplicationService(IReviewManager reviewManager)
    {
        _reviewManager = reviewManager;
    }

    public async Task<GetMovieReviewsDto> GetMovieReviewsAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        GetMovieReviewsDto result = new GetMovieReviewsDto();
        foreach (Review review in await _reviewManager.GetListByMovieAsync(id, cancellationToken))
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