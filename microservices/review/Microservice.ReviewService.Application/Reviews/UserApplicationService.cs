using MediatR;
using Microservice.Core.Application.Services;
using Microservice.ReviewService.Reviews.Queries;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

internal class UserApplicationService : ApplicationService, IUserApplicationService
{
    private readonly IMediator _mediator;

    public UserApplicationService(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<GetUserReviewsDto> GetUserReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        GetUserReviewsQuery query = new GetUserReviewsQuery { UserId = id, PageIndex = pageIndex, PageSize = pageSize };
        return await _mediator.Send(query, cancellationToken);
    }
}