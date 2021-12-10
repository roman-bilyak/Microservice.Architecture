namespace Microservice.ReviewService.Application.Reviews;

public interface IMovieApplicationService
{
    public Task<GetMovieReviewsDto> GetMovieReviewsAsync(Guid id, CancellationToken cancellationToken);
}