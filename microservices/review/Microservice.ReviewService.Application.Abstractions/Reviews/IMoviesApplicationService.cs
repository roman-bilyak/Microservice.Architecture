using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public interface IMoviesApplicationService : IApplicationService
{
    public Task<GetMovieReviewsDto> GetListReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    Task<ReviewDto> GetReviewsAsync([Required] Guid id, [Required] Guid reviewId, CancellationToken cancellationToken);

    Task<ReviewDto> CreateReviewsAsync([Required] Guid id, CreateReviewDto review, CancellationToken cancellationToken);

    Task DeleteReviewsAsync([Required] Guid id, [Required] Guid reviewId, CancellationToken cancellationToken);
}