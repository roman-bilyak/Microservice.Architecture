using Microservice.ReviewService.Domain.Reviews;

namespace Microservice.ReviewService.Application.Reviews;

public class ReviewDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid MovieId { get; set; }

    public string Text { get; set; }

    public RatingEnum Rating { get; set; }
}