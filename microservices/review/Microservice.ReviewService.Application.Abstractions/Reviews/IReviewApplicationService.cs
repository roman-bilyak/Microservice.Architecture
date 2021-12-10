namespace Microservice.ReviewService.Application.Reviews;

public interface IReviewApplicationService
{
    public Task<ReviewDto> GetReviewAsync(Guid id, CancellationToken cancellationToken);

    public Task<ReviewDto> CreateReviewAsync(CreateReviewDto review, CancellationToken cancellationToken);

    public Task DeleteMovieAsync(Guid id, CancellationToken cancellationToken);
}