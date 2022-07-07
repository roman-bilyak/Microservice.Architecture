using MassTransit;
using Microservice.Application.CQRS.Queries;

namespace Microservice.ReviewService.Reviews.Queries
{
    public class GetUserReviewsQuery : ListQuery
    {
        public Guid UserId { get; init; }

        public class GetUserReviewsQueryHandler : IQueryHandler<GetUserReviewsQuery>
        {
            private readonly IReviewManager _reviewManager;

            public GetUserReviewsQueryHandler(IReviewManager reviewManager)
            {
                _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
            }

            public async Task Consume(ConsumeContext<GetUserReviewsQuery> context)
            {
                GetUserReviewsDto result = new GetUserReviewsDto
                {
                    TotalCount = await _reviewManager.GetCountByUserAsync(context.Message.UserId, context.CancellationToken)
                };

                List<Review> reviews = await _reviewManager.GetListByUserAsync(context.Message.UserId, context.Message.PageIndex, context.Message.PageSize, context.CancellationToken);
                foreach (Review review in reviews)
                {
                    result.Items.Add(new ReviewDto
                    {
                        Id = review.Id,
                        UserId = review.UserId,
                        MovieId = review.MovieId,
                        Text = review.Text,
                        Rating = review.Rating
                    });
                }
                await context.RespondAsync(result);
            }
        }
    }
}