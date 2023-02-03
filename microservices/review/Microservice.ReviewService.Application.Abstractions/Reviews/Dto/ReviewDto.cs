namespace Microservice.ReviewService.Reviews;

public record ReviewDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid MovieId { get; init; }

    public string Text { get; init; } = string.Empty;

    public RatingEnum Rating { get; init; }
}