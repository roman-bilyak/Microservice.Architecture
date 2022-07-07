using MassTransit;
using Microservice.Application.CQRS.Queries;

namespace Microservice.ReviewService.Reviews.Queries
{
    public class GetReviewByIdQuery : ItemQuery<Guid>
    {
        public class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery>
        {
            private readonly IReviewManager _reviewManager;

            public GetReviewByIdQueryHandler(IReviewManager reviewManager)
            {
                _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
            }

            public async Task Consume(ConsumeContext<GetReviewByIdQuery> context)
            {
                Review review = await _reviewManager.GetByIdAsync(context.Message.Id, context.CancellationToken);
                if (review == null)
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
}