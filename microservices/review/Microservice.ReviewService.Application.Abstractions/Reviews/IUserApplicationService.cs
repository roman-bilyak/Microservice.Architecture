namespace Microservice.ReviewService.Reviews;

public interface IUserApplicationService : IReviewModuleApplicationService
{
    public Task<GetUserReviewsDto> GetUserReviewsAsync(Guid id, CancellationToken cancellationToken);
}