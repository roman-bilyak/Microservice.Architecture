using Microservice.Core.CQRS.Queries;

namespace Microservice.ReviewService.Reviews.Queries
{
    internal class GetReviewByIdQuery : ItemQuery<Guid, ReviewDto>
    {
        internal class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery, ReviewDto>
        {
            private readonly IReviewManager _reviewManager;

            public GetReviewByIdQueryHandler(IReviewManager reviewManager)
            {
                _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
            }

            public async Task<ReviewDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
            {
                Review review = await _reviewManager.GetByIdAsync(request.Id, cancellationToken);
                if (review == null)
                {
                    throw new Exception($"Review (id = '{request.Id}') not found");
                }

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