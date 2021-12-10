namespace Microservice.MovieService.Application.MovieManagement;

public interface IMovieApplicationService
{
    public Task<MovieDto> GetMovieAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<List<MovieDto>> GetMoviesAsync(CancellationToken cancellationToken = default);

    public Task<MovieDto> CreateMovieAsync(CreateMovieDto movie, CancellationToken cancellationToken = default);

    public Task<MovieDto> UpdateMovieAsync(Guid id, UpdateMovieDto movie, CancellationToken cancellationToken = default);

    public Task DeleteMovieAsync(Guid id, CancellationToken cancellationToken = default);

}