using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public interface IMovieApplicationService : IApplicationService
{
    public Task<MovieReviewListDto> GetReviewListAsync([Required] Guid movieId, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    Task<ReviewDto> GetReviewAsync([Required] Guid movieId, [Required] Guid reviewId, CancellationToken cancellationToken);

    Task<ReviewDto> CreateReviewAsync([Required] Guid movieId, CreateReviewDto review, CancellationToken cancellationToken);

    Task DeleteReviewAsync([Required] Guid movieId, [Required] Guid reviewId, CancellationToken cancellationToken);
}