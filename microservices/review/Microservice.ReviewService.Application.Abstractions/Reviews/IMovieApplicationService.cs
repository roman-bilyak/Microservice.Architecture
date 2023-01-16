using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public interface IMovieApplicationService : IApplicationService
{
    public Task<GetMovieReviewsDto> GetReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);
}