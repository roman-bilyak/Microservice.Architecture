using Microservice.Application;

namespace Microservice.ReviewService.Reviews;

public interface IUserApplicationService : IApplicationService
{
    /// <summary>
    /// Retrieves a paginated list of reviews for a user based on the provided user id, page index, and page size.
    /// </summary>
    /// <param name="userId">The id of the user to retrieve the reviews for.</param>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A paginated list of reviews for the user.</returns>
    public Task<UserReviewListDto> GetReviewListAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
}