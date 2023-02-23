using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class DeleteReviewCommand : DeleteCommand<Guid>
{
    public Guid MovieId { get; protected set; }

    public DeleteReviewCommand(Guid movieId, Guid id) : base(id)
    {
        MovieId = movieId;
    }

    public class DeleteReviewCommandHandler : CommandHandler<DeleteReviewCommand>
    {
        private readonly IReviewManager _reviewManager;

        public DeleteReviewCommandHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        protected override async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewManager.FindByIdAsync(request.Id, cancellationToken);
            if (review is null)
            {
                throw new EntityNotFoundException(typeof(Review), request.Id);
            }

            if (review.UserId != Guid.Empty)
            {
                //TODO: check current user id
                throw new Exception($"Review can't be deleted");
            }

            await _reviewManager.DeleteAsync(review, cancellationToken);
            await _reviewManager.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}