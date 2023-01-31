using System.ComponentModel.DataAnnotations;

namespace Microservice.ReviewService.Reviews;

public record CreateReviewDto
{
    [Required]
    [MaxLength(500)]
    public string Text { get; init; } = string.Empty;

    public RatingEnum Rating { get; init; }
}