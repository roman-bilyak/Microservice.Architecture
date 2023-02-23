using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class CreateReviewCommand : CreateCommand<CreateReviewDto, ReviewDto>
{
    public Guid MovieId { get; protected set; }

    public CreateReviewCommand(Guid movieId, CreateReviewDto model) : base(model)
    {
        MovieId = movieId;
    }

    public class CreateReviewCommandHandler : CommandHandler<CreateReviewCommand, ReviewDto>
    {
        private readonly IReviewManager _reviewManager;

        public CreateReviewCommandHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        protected override async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            CreateReviewDto reviewDto = request.Model;
            Guid userId = Guid.Empty; //TODO: use current user id
            Review review = new(Guid.NewGuid(), userId, request.MovieId, reviewDto.Text, reviewDto.Rating);

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