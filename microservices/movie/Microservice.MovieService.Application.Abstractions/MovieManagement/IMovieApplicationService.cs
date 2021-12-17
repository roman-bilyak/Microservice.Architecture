using Microservice.Core.Services;

namespace Microservice.MovieService.MovieManagement;

public interface IMovieApplicationService : IApplicationService
{
    public Task<MovieDto> GetMovieAsync(Guid id, CancellationToken cancellationToken);

    public Task<List<MovieDto>> GetMoviesAsync(CancellationToken cancellationToken);

    public Task<MovieDto> CreateMovieAsync(CreateMovieDto movie, CancellationToken cancellationToken);

    public Task<MovieDto> UpdateMovieAsync(Guid id, UpdateMovieDto movie, CancellationToken cancellationToken);

    public Task DeleteMovieAsync(Guid id, CancellationToken cancellationToken);
}