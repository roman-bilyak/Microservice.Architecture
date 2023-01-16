using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

[Authorize]
internal class MoviesApplicationService : ApplicationService, IMoviesApplicationService
{
    private readonly IMediator _mediator;

    public MoviesApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<GetMovieReviewsDto> GetListReviewsAsync([Required] Guid id, [Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetMovieReviewsQuery>();
        var response = await client.GetResponse<GetMovieReviewsDto>(new GetMovieReviewsQuery(id, pageIndex, pageSize), cancellationToken);
        return response.Message;
    }

    public async Task<ReviewDto> GetReviewsAsync([Required] Guid id, [Required] Guid reviewId, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetReviewByIdQuery>();
        var response = await client.GetResponse<ReviewDto>(new GetReviewByIdQuery(id, reviewId), cancellationToken);
        return response.Message;
    }

    public async Task<ReviewDto> CreateReviewsAsync([Required] Guid id, CreateReviewDto review, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateReviewCommand>();
        var response = await client.GetResponse<ReviewDto>(new CreateReviewCommand(id, review), cancellationToken);
        return response.Message;
    }

    public async Task DeleteReviewsAsync([Required] Guid id, [Required] Guid reviewId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteReviewCommand(id, reviewId), cancellationToken);
    }
}