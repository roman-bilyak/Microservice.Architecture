using Microservice.Application.CQRS.Queries;

namespace Microservice.ReviewService.Reviews.Queries
{
    internal class GetUserReviewsQuery : ListQuery<GetUserReviewsDto>
    {
        public Guid UserId { get; init; }

        internal class GetUserReviewsQueryHandler : IQueryHandler<GetUserReviewsQuery, GetUserReviewsDto>
        {
            private readonly IReviewManager _reviewManager;

            public GetUserReviewsQueryHandler(IReviewManager reviewManager)
            {
                _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
            }

            public async Task<GetUserReviewsDto> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
            {
                GetUserReviewsDto result = new GetUserReviewsDto
                {
                    TotalCount = await _reviewManager.GetCountByUserAsync(request.UserId, cancellationToken)
                };

                foreach (Review review in await _reviewManager.GetListByUserAsync(request.UserId, request.PageIndex, request.PageSize, cancellationToken))
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
                return result;
            }
        }
    }
}