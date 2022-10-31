using MassTransit;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class DeleteReviewCommand : DeleteCommand<Guid>
{
    public class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand>
    {
        private readonly IReviewManager _reviewManager;

        public DeleteReviewCommandHandler(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
        }

        public async Task Consume(ConsumeContext<DeleteReviewCommand> context)
        {
            Review review = await _reviewManager.GetByIdAsync(context.Message.Id, context.CancellationToken);
            if (review == null)
            {
                throw new Exception($"Review (id = '{context.Message.Id}') not found");
            }

            if (review.UserId != Guid.Empty)
            {
                //TODO: check current user id
                throw new Exception($"Review can't be deleted");
            }

            await _reviewManager.DeleteAsync(review, context.CancellationToken);
            await _reviewManager.SaveChangesAsync(context.CancellationToken);
        }
    }
}