using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public interface IUserApplicationService : IApplicationService
{
    public Task<UserReviewListDto> GetReviewListAsync([Required] Guid userId, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);
}