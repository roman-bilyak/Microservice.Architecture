namespace Microservice.ReviewService.Reviews;

public class UpdateReviewDto
{
    public string Text { get; init; } = string.Empty;

    public RatingEnum Rating { get; init; }
}