using MassTransit;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class DeleteReviewCommand : DeleteCommand<Guid>
{
    public DeleteReviewCommand(Guid id) : base(id)
    {
    }

    public class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand>
    {
        private readonly IReviewManager _reviewManager;

        public DeleteReviewCommandHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        public async Task Consume(ConsumeContext<DeleteReviewCommand> context)
        {
            Review? review = await _reviewManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (review is null)
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