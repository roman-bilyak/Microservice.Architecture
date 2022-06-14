using Microservice.Core.Services;
using Microservice.Core.Web;
using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.MovieManagement;

internal class MovieApplicationService : ApplicationService, IMovieApplicationService
{
    private readonly IMovieManager _movieManager;

    public MovieApplicationService(IMovieManager movieManager)
    {
        _movieManager = movieManager;
    }

    public async Task<MovieDto> GetMovieAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        Movie movie = await _movieManager.GetByIdAsync(id, cancellationToken);
        if (movie == null)
        {
            throw new Exception($"Movie (id = '{id}') not found");
        }

        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title
        };
    }

    public async Task<MovieListDto> GetMoviesAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        List<Movie> movies = await _movieManager.ListAsync(pageIndex, pageSize, cancellationToken);
        int totalCount = await _movieManager.CountAsync(cancellationToken);

        List<MovieDto> items = new List<MovieDto>();
        foreach (Movie movie in movies)
        {
            items.Add(new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
            });
        }

        return new MovieListDto
        {
            Items = items,
            TotalCount = totalCount,
        };
    }

    public async Task<MovieDto> CreateMovieAsync([Required] CreateMovieDto movie, CancellationToken cancellationToken)
    {
        Movie entity = new Movie
        {
            Title = movie.Title
        };

        entity = await _movieManager.AddAsync(entity, cancellationToken);
        await _movieManager.SaveChangesAsync(cancellationToken);

        return new MovieDto
        {
            Id = entity.Id,
            Title = movie.Title
        };
    }

    public async Task<MovieDto> UpdateMovieAsync([Required] Guid id, [Required] UpdateMovieDto movie, CancellationToken cancellationToken)
    {
        Movie entity = await _movieManager.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new Exception($"Movie (id = '{id}') not found");
        }

        entity.Title = movie.Title;

        entity = await _movieManager.UpdateAsync(entity, cancellationToken);
        await _movieManager.SaveChangesAsync(cancellationToken);

        return new MovieDto
        {
            Id = entity.Id,
            Title = entity.Title
        };
    }

    public async Task DeleteMovieAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        Movie entity = await _movieManager.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new Exception($"Movie (id = '{id}') not found");
        }

        await _movieManager.DeleteAsync(entity, cancellationToken);
        await _movieManager.SaveChangesAsync(cancellationToken);
    }
}