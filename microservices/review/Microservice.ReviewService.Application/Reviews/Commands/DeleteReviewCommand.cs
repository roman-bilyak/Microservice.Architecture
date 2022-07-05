using MediatR;
using Microservice.Application.CQRS.Commands;

namespace Microservice.ReviewService.Reviews.Commands
{
    internal class DeleteReviewCommand : DeleteCommand<Guid>
    {
        internal class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand>
        {
            private readonly IReviewManager _reviewManager;

            public DeleteReviewCommandHandler(IReviewManager reviewManager)
            {
                _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
            }

            public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
            {
                Review review = await _reviewManager.GetByIdAsync(request.Id, cancellationToken);
                if (review == null)
                {
                    throw new Exception($"Review (id = '{request.Id}') not found");
                }

                if (review.UserId != Guid.Empty)
                {
                    //TODO: check current user id
                    throw new Exception($"Review (id = '{request.Id}') not found");
                }

                await _reviewManager.DeleteAsync(review, cancellationToken);
                await _reviewManager.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}