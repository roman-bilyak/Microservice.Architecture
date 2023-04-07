using MassTransit;
using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.ReviewService.Reviews;

[Authorize]
internal class UserApplicationService : ApplicationService, IUserApplicationService
{
    private readonly IMediator _mediator;

    public UserApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<UserReviewListDto> GetReviewListAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetUserReviewsQuery(userId, pageIndex, pageSize), cancellationToken);
    }
}