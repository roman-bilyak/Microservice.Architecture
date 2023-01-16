using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public interface IUserApplicationService : IApplicationService
{
    public Task<GetUserReviewsDto> GetReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);
}