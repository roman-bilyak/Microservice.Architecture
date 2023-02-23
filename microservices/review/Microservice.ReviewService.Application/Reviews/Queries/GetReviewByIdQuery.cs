﻿using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class GetReviewByIdQuery : ItemQuery<Guid, ReviewDto>
{
    public Guid MovieId { get; protected set; }

    public GetReviewByIdQuery(Guid movieId, Guid id) : base(id)
    {
        MovieId = movieId;
    }

    public class GetReviewByIdQueryHandler : QueryHandler<GetReviewByIdQuery, ReviewDto>
    {
        private readonly IReviewManager _reviewManager;

        public GetReviewByIdQueryHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        protected override async Task<ReviewDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewManager.FindByIdAsync(request.Id, cancellationToken);
            if (review is null)
            {
                throw new EntityNotFoundException(typeof(Review), request.Id);
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