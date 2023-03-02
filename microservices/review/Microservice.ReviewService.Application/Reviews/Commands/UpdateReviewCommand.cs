using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class UpdateReviewCommand : UpdateCommand<Guid, UpdateReviewDto, ReviewDto>
{
    public Guid MovieId { get; protected set; }

    public UpdateReviewCommand(Guid movieId, Guid id, UpdateReviewDto model) : base(id, model)
    {
        MovieId = movieId;
    }

    public class UpdateMovieCommandHandler : CommandHandler<UpdateReviewCommand, ReviewDto>
    {
        private readonly IReviewManager _reviewManager;

        public UpdateMovieCommandHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        protected override async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewManager.GetByIdAsync(request.MovieId, request.Id, cancellationToken);
            if (review is null || review.MovieId != request.MovieId)
            {
                throw new EntityNotFoundException(typeof(Review), request.Id);
            }

            review.Update(request.Model.Comment, request.Model.Rating);

            review = await _reviewManager.UpdateAsync(review, cancellationToken);
            await _reviewManager.SaveChangesAsync(cancellationToken);

            return new ReviewDto
            {
                Id = review.Id,
                MovieId = review.MovieId,
                UserId = review.UserId,
                Comment = review.Comment,
                Rating = review.Rating,
            };
        }
    }
}