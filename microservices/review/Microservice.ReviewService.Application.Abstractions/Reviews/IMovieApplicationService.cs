using Microservice.Core.Services;

namespace Microservice.ReviewService.Reviews;

public interface IMovieApplicationService : IApplicationService
{
    public Task<GetMovieReviewsDto> GetMovieReviewsAsync(Guid id, CancellationToken cancellationToken);
}