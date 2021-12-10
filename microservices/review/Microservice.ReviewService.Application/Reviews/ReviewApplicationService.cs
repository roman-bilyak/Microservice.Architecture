using Microservice.Core.Services;
using Microservice.ReviewService.Domain.Reviews;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Application.Reviews;

internal class ReviewApplicationService : ApplicationService, IReviewApplicationService
{
    private readonly IReviewManager _reviewManager;

    public ReviewApplicationService(IReviewManager reviewManager)
    {
        _reviewManager = reviewManager;
    }

    public async Task<ReviewDto> GetReviewAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        Review review = await _reviewManager.GetByIdAsync(id, cancellationToken);
        if (review == null)
        {
            throw new Exception($"Review (id = '{id}') not found");
        }

        return new ReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            MovieId = review.MovieId,
            Text = review.Text,
            Rating = review.Rating
        };
    }

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto review, CancellationToken cancellationToken)
    {
        Review entity = new Review
        {
            UserId = Guid.Empty, //TODO: use current user id
            MovieId = review.MovieId,
            Text = review.Text,
            Rating = review.Rating
        };

        entity = await _reviewManager.AddAsync(entity, cancellationToken);
        await _reviewManager.SaveChangesAsync(cancellationToken);

        return new ReviewDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            MovieId = entity.MovieId,
            Text = entity.Text,
            Rating = entity.Rating
        };
    }

    public async Task DeleteMovieAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        Review review = await _reviewManager.GetByIdAsync(id, cancellationToken);
        if (review == null)
        {
            throw new Exception($"Review (id = '{id}') not found");
        }

        await _reviewManager.DeleteAsync(review, cancellationToken);
        await _reviewManager.SaveChangesAsync(cancellationToken);
    }
}
