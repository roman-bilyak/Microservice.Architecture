using MediatR;
using Microservice.Core.Application.Services;
using Microservice.ReviewService.Reviews.Queries;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

internal class MovieApplicationService : ApplicationService, IMovieApplicationService
{
    private readonly IMediator _mediator;

    public MovieApplicationService(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<GetMovieReviewsDto> GetMovieReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        GetMovieReviewsQuery query = new GetMovieReviewsQuery { MovieId = id, PageIndex = pageIndex, PageSize = pageSize };
        return await _mediator.Send(query, cancellationToken);
    }
}