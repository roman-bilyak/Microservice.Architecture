namespace Microservice.ReviewService.Reviews;

public record CreateReviewDto
{
    public string Text { get; init; } = string.Empty;

    public RatingEnum Rating { get; init; }
}