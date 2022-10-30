using MassTransit.Mediator;
using Microservice.Application;
using Microservice.ReviewService.Reviews.Queries;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

[Authorize]
internal class UserApplicationService : ApplicationService, IUserApplicationService
{
    private readonly IMediator _mediator;

    public UserApplicationService(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<GetUserReviewsDto> GetUserReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserReviewsQuery>();
        var response = await client.GetResponse<GetUserReviewsDto>(new GetUserReviewsQuery { UserId = id, PageIndex = pageIndex, PageSize = pageSize }, cancellationToken);
        return response.Message;
    }
}