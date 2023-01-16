using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.Movies;

public interface IMovieApplicationService : IApplicationService
{
    public Task<MovieDto> GetAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task<MovieListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<MovieDto> CreateAsync([Required] CreateMovieDto movie, CancellationToken cancellationToken);

    public Task<MovieDto> UpdateAsync([Required] Guid id, [Required] UpdateMovieDto movie, CancellationToken cancellationToken);

    public Task DeleteAsync([Required] Guid id, CancellationToken cancellationToken);
}