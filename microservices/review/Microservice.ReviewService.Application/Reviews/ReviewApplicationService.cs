using MediatR;
using Microservice.Core.Application.Services;
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
        GetReviewByIdQuery query = new GetReviewByIdQuery { Id = id };
        return await _mediator.Send(query, cancellationToken);
    }

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto review, CancellationToken cancellationToken)
    {
        CreateReviewCommand command = new CreateReviewCommand { Model = review };
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task DeleteReviewAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        DeleteReviewCommand command = new DeleteReviewCommand { Id = id };
        await _mediator.Send(command, cancellationToken);
    }
}
