using MassTransit;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class GetReviewByIdQuery : ItemQuery<Guid>
{
    public Guid MovieId { get; protected set; }

    public GetReviewByIdQuery(Guid movieId, Guid id) : base(id)
    {
        MovieId = movieId;
    }

    public class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery>
    {
        private readonly IReviewManager _reviewManager;

        public GetReviewByIdQueryHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        public async Task Consume(ConsumeContext<GetReviewByIdQuery> context)
        {
            Review? review = await _reviewManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (review is null)
            {
                throw new Exception($"Review (id = '{context.Message.Id}') not found");
            }

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