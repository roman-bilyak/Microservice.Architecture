namespace Microservice.ReviewService.Application.Reviews;

public interface IUserApplicationService
{
    public Task<GetUserReviewsDto> GetUserReviewsAsync(Guid id, CancellationToken cancellationToken);
}