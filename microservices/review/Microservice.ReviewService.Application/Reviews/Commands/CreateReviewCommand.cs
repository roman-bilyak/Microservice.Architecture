using MassTransit;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class CreateReviewCommand : CreateCommand<CreateReviewDto>
{
    public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand>
    {
        private readonly IReviewManager _reviewManager;

        public CreateReviewCommandHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        public async Task Consume(ConsumeContext<CreateReviewCommand> context)
        {
            CreateReviewDto reviewDto = context.Message.Model;
            Guid userId = Guid.Empty; //TODO: use current user id
            Review review = new Review(userId, reviewDto.MovieId, reviewDto.Text, reviewDto.Rating);

            review = await _reviewManager.AddAsync(review, context.CancellationToken);
            await _reviewManager.SaveChangesAsync(context.CancellationToken);

            await context.RespondAsync(new ReviewDto
            {
                Id = review.Id,
                UserId = review.UserId,
                MovieId = review.MovieId,
                Text = review.Text,
                Rating = review.Rating
            });
        }
    }
}