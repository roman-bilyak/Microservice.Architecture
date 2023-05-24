using Microservice.Application;

namespace Microservice.ReviewService.Reviews;

public class GetMovieReviewsQuery : ListQuery<MovieReviewListDto>
{
    public Guid MovieId { get; protected set; }

    public GetMovieReviewsQuery(Guid movieId, int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
        MovieId = movieId;
    }

    public class GetMovieReviewsQueryHandler : QueryHandler<GetMovieReviewsQuery, MovieReviewListDto>
    {
        private readonly IReviewManager _reviewManager;

        public GetMovieReviewsQueryHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        protected override async Task<MovieReviewListDto> Handle(GetMovieReviewsQuery request, CancellationToken cancellationToken)
        {
            MovieReviewListDto result = new()
            {
                TotalCount = await _reviewManager.GetCountByMovieAsync(request.MovieId, cancellationToken)
            };

            List<Review> reviews = await _reviewManager.GetListByMovieAsync(request.MovieId, request.PageIndex, request.PageSize, cancellationToken);
            foreach (Review review in reviews)
            {
                result.Items.Add(new ReviewDto
                {
                    Id = review.Id,
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    Comment = review.Comment,
                    Rating = review.Rating
                });
            }
            return result;
        }
    }
}