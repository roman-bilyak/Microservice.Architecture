namespace Microservice.MovieService.Domain;

public interface IMovieManager
{
    Task<Movie> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Movie>> ListAsync(CancellationToken cancellationToken);

    Task<Movie> AddAsync(Movie movie, CancellationToken cancellationToken);

    Task<Movie> UpdateAsync(Movie movie, CancellationToken cancellationToken);

    Task DeleteAsync(Movie movie, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}