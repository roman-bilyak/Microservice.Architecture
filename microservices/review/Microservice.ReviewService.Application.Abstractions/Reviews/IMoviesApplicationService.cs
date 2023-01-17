using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public interface IMoviesApplicationService : IApplicationService
{
    public Task<GetMovieReviewsDto> GetListReviewsAsync([Required] Guid movieId, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    Task<ReviewDto> GetReviewsAsync([Required] Guid movieId, [Required] Guid reviewId, CancellationToken cancellationToken);

    Task<ReviewDto> CreateReviewsAsync([Required] Guid movieId, CreateReviewDto review, CancellationToken cancellationToken);

    Task DeleteReviewsAsync([Required] Guid movieId, [Required] Guid reviewId, CancellationToken cancellationToken);
}