using MassTransit;
using MassTransit.Mediator;
using Microservice.Application.Services;
using Microservice.ReviewService.Reviews.Commands;
using Microservice.ReviewService.Reviews.Queries;
using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

internal class ReviewApplicationService : ApplicationService, IReviewApplicationService
{
    private readonly IMediator _mediator;

    public ReviewApplicationService(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ReviewDto> GetReviewAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetReviewByIdQuery>();
        var response = await client.GetResponse<ReviewDto>(new GetReviewByIdQuery { Id = id }, cancellationToken);
        return response.Message;
    }

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto review, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateReviewCommand>();
        var response = await client.GetResponse<ReviewDto>(new CreateReviewCommand { Model = review }, cancellationToken);
        return response.Message;
    }

    public async Task DeleteReviewAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteReviewCommand { Id = id }, cancellationToken);
    }
}