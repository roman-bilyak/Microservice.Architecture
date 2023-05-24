namespace Microservice.MovieService.Movies;

public record CreateMovieDto
{
    public string Title { get; init; } = string.Empty;
}