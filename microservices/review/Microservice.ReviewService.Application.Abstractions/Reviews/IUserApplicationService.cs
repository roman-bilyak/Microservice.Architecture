using Microservice.Core.Services;

namespace Microservice.ReviewService.Reviews;

public interface IUserApplicationService : IApplicationService
{
    public Task<GetUserReviewsDto> GetUserReviewsAsync(Guid id, CancellationToken cancellationToken);
}