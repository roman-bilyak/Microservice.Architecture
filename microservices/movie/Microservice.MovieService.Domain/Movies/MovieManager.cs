using Microservice.Core.Database;

namespace Microservice.MovieService.Movies;

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

    public async Task<List<Movie>> ListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        Specification<Movie> specification = new Specification<Movie>();
        specification.ApplyPaging(pageIndex, pageSize);

        return await _movieRepository.ListAsync(specification, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _movieRepository.CountAsync(cancellationToken);
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