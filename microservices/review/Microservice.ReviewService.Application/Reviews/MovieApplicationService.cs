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
        var client = _mediator.CreateRequestClient<GetMovieReviewsQuery>();
        var response = await client.GetResponse<MovieReviewListDto>(new GetMovieReviewsQuery(movieId, pageIndex, pageSize), cancellationToken);
        return response.Message;
    }

    public async Task<ReviewDto> GetReviewAsync(Guid movieId, Guid reviewId, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetReviewByIdQuery>();
        var response = await client.GetResponse<ReviewDto>(new GetReviewByIdQuery(movieId, reviewId), cancellationToken);
        return response.Message;
    }

    public async Task<ReviewDto> CreateReviewAsync(Guid movieId, CreateReviewDto review, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateReviewCommand>();
        var response = await client.GetResponse<ReviewDto>(new CreateReviewCommand(movieId, review), cancellationToken);
        return response.Message;
    }

    public async Task<ReviewDto> UpdateReviewAsync(Guid movieId, Guid reviewId, UpdateReviewDto review, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<UpdateReviewCommand>();
        var response = await client.GetResponse<ReviewDto>(new UpdateReviewCommand(movieId, reviewId, review), cancellationToken);
        return response.Message;
    }

    public async Task DeleteReviewAsync(Guid movieId, Guid reviewId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteReviewCommand(movieId, reviewId), cancellationToken);
    }
}