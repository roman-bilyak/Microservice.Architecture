namespace Microservice.ReviewService.Reviews;

public class UpdateReviewDto
{
    public string Comment { get; init; } = string.Empty;

    public RatingEnum Rating { get; init; }
}