namespace Microservice.ReviewService.Reviews;

public interface IMovieApplicationService
{
    public Task<GetMovieReviewsDto> GetMovieReviewsAsync(Guid id, CancellationToken cancellationToken);
}