using Microservice.Application;

namespace Microservice.ReviewService.Reviews;

public interface IReviewApplicationService : IApplicationService
{
    public Task<ReviewDto> GetAsync(Guid id, CancellationToken cancellationToken);

    public Task<ReviewDto> CreateAsync(CreateReviewDto review, CancellationToken cancellationToken);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}