using Microservice.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.MovieManagement;

public interface IMovieApplicationService : IApplicationService
{
    public Task<MovieDto> GetMovieAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task<MovieListDto> GetMoviesAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<MovieDto> CreateMovieAsync([Required] CreateMovieDto movie, CancellationToken cancellationToken);

    public Task<MovieDto> UpdateMovieAsync([Required] Guid id, [Required] UpdateMovieDto movie, CancellationToken cancellationToken);

    public Task DeleteMovieAsync([Required] Guid id, CancellationToken cancellationToken);
}