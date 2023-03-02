namespace Microservice.ReviewService.Reviews;

public record CreateReviewDto
{
    public string Comment { get; init; } = string.Empty;

    public RatingEnum Rating { get; init; }
}