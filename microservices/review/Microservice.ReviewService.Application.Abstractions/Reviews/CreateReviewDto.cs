namespace Microservice.ReviewService.Reviews;

public class CreateReviewDto
{
    public string Text { get; set; }

    public RatingEnum Rating { get; set; }
}