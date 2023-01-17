using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.Movies;

public interface IMoviesApplicationService : IApplicationService
{
    public Task<MovieListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<MovieDto> GetAsync([Required] Guid movieId, CancellationToken cancellationToken);

    public Task<MovieDto> CreateAsync([Required] CreateMovieDto movie, CancellationToken cancellationToken);

    public Task<MovieDto> UpdateAsync([Required] Guid movieId, [Required] UpdateMovieDto movie, CancellationToken cancellationToken);

    public Task DeleteAsync([Required] Guid movieId, CancellationToken cancellationToken);
}