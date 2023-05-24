using MassTransit;
using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.ReviewService.Reviews;

[Authorize]
internal class MovieApplicationService : ApplicationService, IMovieApplicationService
{
    private readonly IMediator _mediator;

    public MovieApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<MovieReviewListDto> GetReviewListAsync(Guid movieId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetMovieReviewsQuery(movieId, pageIndex, pageSize), cancellationToken);
    }

    public async Task<ReviewDto> GetReviewAsync(Guid movieId, Guid reviewId, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetReviewByIdQuery(movieId, reviewId), cancellationToken);
    }

    public async Task<ReviewDto> CreateReviewAsync(Guid movieId, CreateReviewDto review, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new CreateReviewCommand(movieId, review), cancellationToken);
    }

    public async Task<ReviewDto> UpdateReviewAsync(Guid movieId, Guid reviewId, UpdateReviewDto review, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new UpdateReviewCommand(movieId, reviewId, review), cancellationToken);
    }

    public async Task DeleteReviewAsync(Guid movieId, Guid reviewId, CancellationToken cancellationToken)
    {
        await _mediator.SendRequest(new DeleteReviewCommand(movieId, reviewId), cancellationToken);
    }
}