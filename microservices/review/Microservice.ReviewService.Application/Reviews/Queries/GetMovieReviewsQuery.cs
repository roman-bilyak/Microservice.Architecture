using Microservice.Core.CQRS.Queries;

namespace Microservice.ReviewService.Reviews.Queries
{
    internal class GetMovieReviewsQuery : ListQuery<GetMovieReviewsDto>
    {
        public Guid MovieId { get; set; }

        internal class GetMovieReviewsQueryHandler : IQueryHandler<GetMovieReviewsQuery, GetMovieReviewsDto>
        {
            private readonly IReviewManager _reviewManager;

            public GetMovieReviewsQueryHandler(IReviewManager reviewManager)
            {
                _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));
            }

            public async Task<GetMovieReviewsDto> Handle(GetMovieReviewsQuery request, CancellationToken cancellationToken)
            {
                GetMovieReviewsDto result = new GetMovieReviewsDto
                {
                    TotalCount = await _reviewManager.GetCountByMovieAsync(request.MovieId, cancellationToken)
                };

                foreach (Review review in await _reviewManager.GetListByMovieAsync(request.MovieId, request.PageIndex, request.PageSize, cancellationToken))
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