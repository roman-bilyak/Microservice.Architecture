using Microservice.Application;

namespace Microservice.ReviewService.Reviews;

public class GetUserReviewsQuery : ListQuery<UserReviewListDto>
{
    public Guid UserId { get; protected set; }

    public GetUserReviewsQuery(Guid userId, int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
        UserId = userId;
    }

    public class GetUserReviewsQueryHandler : QueryHandler<GetUserReviewsQuery, UserReviewListDto>
    {
        private readonly IReviewManager _reviewManager;

        public GetUserReviewsQueryHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        protected override async Task<UserReviewListDto> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
        {
            UserReviewListDto result = new()
            {
                TotalCount = await _reviewManager.GetCountByUserAsync(request.UserId, cancellationToken)
            };

            List<Review> reviews = await _reviewManager.GetListByUserAsync(request.UserId, request.PageIndex, request.PageSize, cancellationToken);
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