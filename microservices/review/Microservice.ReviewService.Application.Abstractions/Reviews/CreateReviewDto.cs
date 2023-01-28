using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public record CreateReviewDto
{
    public Guid MovieId { get; init; }

    [Required]
    [MaxLength(500)]
    public string Text { get; init; } = string.Empty;

    public RatingEnum Rating { get; init; }
}