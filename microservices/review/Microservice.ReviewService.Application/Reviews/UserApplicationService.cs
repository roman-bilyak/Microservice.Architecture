using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

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

    public async Task<GetUserReviewsDto> GetUserReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserReviewsQuery>();
        var response = await client.GetResponse<GetUserReviewsDto>(new GetUserReviewsQuery { UserId = id, PageIndex = pageIndex, PageSize = pageSize }, cancellationToken);
        return response.Message;
    }
}