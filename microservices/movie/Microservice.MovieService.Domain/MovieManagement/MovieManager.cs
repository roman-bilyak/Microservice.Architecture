using Microservice.Infrastructure.Database;

namespace Microservice.MovieService.MovieManagement;

internal class MovieManager : IMovieManager
{
    private readonly IRepository<Movie> _movieRepository;

    public MovieManager(IRepository<Movie> movieRepository)
    {
        ArgumentNullException.ThrowIfNull(movieRepository);

        _movieRepository = movieRepository;
    }

    public async Task<Movie> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _movieRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<Movie>> ListAsync(CancellationToken cancellationToken)
    {
        return await _movieRepository.ListAsync(cancellationToken);
    }

    public async Task<Movie> AddAsync(Movie movie, CancellationToken cancellationToken)
    {
        return await _movieRepository.AddAsync(movie, cancellationToken);
    }

    public async Task<Movie> UpdateAsync(Movie movie, CancellationToken cancellationToken)
    {
        return await _movieRepository.UpdateAsync(movie, cancellationToken);
    }

    public async Task DeleteAsync(Movie movie, CancellationToken cancellationToken)
    {
        await _movieRepository.DeleteAsync(movie, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _movieRepository.SaveChangesAsync(cancellationToken);
    }
}