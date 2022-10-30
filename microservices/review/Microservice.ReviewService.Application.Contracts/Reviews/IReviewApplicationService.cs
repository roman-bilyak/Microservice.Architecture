using Microservice.Application;

namespace Microservice.ReviewService.Reviews;

public interface IReviewApplicationService : IApplicationService
{
    public Task<ReviewDto> GetReviewAsync(Guid id, CancellationToken cancellationToken);

    public Task<ReviewDto> CreateReviewAsync(CreateReviewDto review, CancellationToken cancellationToken);

    public Task DeleteReviewAsync(Guid id, CancellationToken cancellationToken);
}