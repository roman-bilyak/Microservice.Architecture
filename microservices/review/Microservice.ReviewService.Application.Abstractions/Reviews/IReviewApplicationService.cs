using Microservice.Core.Services;

namespace Microservice.ReviewService.Reviews;

public interface IReviewApplicationService : IApplicationService
{
    public Task<ReviewDto> GetReviewAsync(Guid id, CancellationToken cancellationToken);

    public Task<ReviewDto> CreateReviewAsync(CreateReviewDto review, CancellationToken cancellationToken);

    public Task DeleteMovieAsync(Guid id, CancellationToken cancellationToken);
}