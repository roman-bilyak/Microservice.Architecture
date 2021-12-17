namespace Microservice.ReviewService.Reviews;

public interface IMovieApplicationService : IReviewModuleApplicationService
{
    public Task<GetMovieReviewsDto> GetMovieReviewsAsync(Guid id, CancellationToken cancellationToken);
}