namespace Microservice.MovieService.Movies;

public record UpdateMovieDto
{
    public string Title { get; init; } = string.Empty;
}