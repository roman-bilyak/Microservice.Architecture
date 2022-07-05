using Microservice.Application.Services;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public interface IMovieApplicationService : IApplicationService
{
    public Task<GetMovieReviewsDto> GetMovieReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);
}