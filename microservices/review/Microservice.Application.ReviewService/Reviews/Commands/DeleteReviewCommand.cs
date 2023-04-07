using Microservice.Application;
using Microservice.Core;

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
        private readonly ICurrentUser _currentUser;

        public DeleteReviewCommandHandler(IReviewManager reviewManager, ICurrentUser currentUser)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));
            ArgumentNullException.ThrowIfNull(currentUser, nameof(currentUser));

            _reviewManager = reviewManager;
            _currentUser = currentUser;
        }

        protected override async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewManager.GetByIdAsync(request.MovieId, request.Id, cancellationToken);
            if (review is null || review.UserId != _currentUser.Id)
            {
                throw new EntityNotFoundException(typeof(Review), request.Id);
            }

            await _reviewManager.DeleteAsync(review, cancellationToken);
            await _reviewManager.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}