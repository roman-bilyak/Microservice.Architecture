using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

[Authorize]
internal class UsersApplicationService : ApplicationService, IUsersApplicationService
{
    private readonly IMediator _mediator;

    public UsersApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<GetUserReviewsDto> GetReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserReviewsQuery>();
        var response = await client.GetResponse<GetUserReviewsDto>(new GetUserReviewsQuery(id, pageIndex, pageSize), cancellationToken);
        return response.Message;
    }
}