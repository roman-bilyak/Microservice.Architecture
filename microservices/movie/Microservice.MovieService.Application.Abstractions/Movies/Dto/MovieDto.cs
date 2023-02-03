namespace Microservice.MovieService.Movies;

public record MovieDto
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;
}