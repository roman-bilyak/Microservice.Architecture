using Microservice.Core.Domain;
using Microservice.Database;

namespace Microservice.MovieService.Movies;

internal class MovieManager : DomainService, IMovieManager
{
    private readonly IRepository<Movie> _movieRepository;

    public MovieManager(IRepository<Movie> movieRepository)
    {
        ArgumentNullException.ThrowIfNull(movieRepository);

        _movieRepository = movieRepository;
    }

    public async Task<Movie?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        FindMovieByIdSpecification specification = new(id);
        return await _movieRepository.SingleOrDefaultAsync(specification, cancellationToken);
    }

    public async Task<List<Movie>> ListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        ISpecification<Movie> specification = new GetMoviesSpecification()
            .ApplyPaging(pageIndex, pageSize)
            .AsNoTracking();

        return await _movieRepository.ListAsync(specification, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        ISpecification<Movie> specification = new GetMoviesSpecification().AsNoTracking();
        return await _movieRepository.CountAsync(specification, cancellationToken);
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