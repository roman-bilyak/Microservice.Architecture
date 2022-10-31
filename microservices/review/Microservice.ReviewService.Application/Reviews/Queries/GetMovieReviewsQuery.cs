using MassTransit;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class GetMovieReviewsQuery : ListQuery
{
    public Guid MovieId { get; set; }

    public class GetMovieReviewsQueryHandler : IQueryHandler<GetMovieReviewsQuery>
    {
        private readonly IReviewManager _reviewManager;

        public GetMovieReviewsQueryHandler(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
        }

        public async Task Consume(ConsumeContext<GetMovieReviewsQuery> context)
        {
            GetMovieReviewsDto result = new GetMovieReviewsDto
            {
                TotalCount = await _reviewManager.GetCountByMovieAsync(context.Message.MovieId, context.CancellationToken)
            };

            List<Review> reviews = await _reviewManager.GetListByMovieAsync(context.Message.MovieId, context.Message.PageIndex, context.Message.PageSize, context.CancellationToken);
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