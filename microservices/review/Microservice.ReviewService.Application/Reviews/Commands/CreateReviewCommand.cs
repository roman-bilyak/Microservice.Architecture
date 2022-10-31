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
            _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
        }

        public async Task Consume(ConsumeContext<CreateReviewCommand> context)
        {
            Review review = new Review
            {
                UserId = Guid.Empty, //TODO: use current user id
                MovieId = context.Message.Model.MovieId,
                Text = context.Message.Model.Text,
                Rating = context.Message.Model.Rating
            };

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