using Microservice.Core.CQRS.Commands;

namespace Microservice.ReviewService.Reviews.Commands
{
    internal class CreateReviewCommand : CreateCommand<CreateReviewDto, ReviewDto>
    {
        internal class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, ReviewDto>
        {
            private readonly IReviewManager _reviewManager;

            public CreateReviewCommandHandler(IReviewManager reviewManager)
            {
                _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
            }

            public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                Review review = new Review
                {
                    UserId = Guid.Empty, //TODO: use current user id
                    MovieId = request.Model.MovieId,
                    Text = request.Model.Text,
                    Rating = request.Model.Rating
                };

                review = await _reviewManager.AddAsync(review, cancellationToken);
                await _reviewManager.SaveChangesAsync(cancellationToken);

                return new ReviewDto
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    MovieId = review.MovieId,
                    Text = review.Text,
                    Rating = review.Rating
                };
            }
        }
    }
}