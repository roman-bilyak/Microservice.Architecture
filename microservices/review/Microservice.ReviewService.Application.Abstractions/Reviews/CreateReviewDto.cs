namespace Microservice.ReviewService.Reviews;

public class CreateReviewDto
{
    public Guid MovieId { get; set; }

    public string Text { get; set; }

    public RatingEnum Rating { get; set; }
}