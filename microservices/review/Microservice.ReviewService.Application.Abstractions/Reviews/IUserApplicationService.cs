namespace Microservice.ReviewService.Reviews;

public interface IUserApplicationService
{
    public Task<GetUserReviewsDto> GetUserReviewsAsync(Guid id, CancellationToken cancellationToken);
}