using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.Movies;

public record UpdateMovieDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; init; } = string.Empty;
}