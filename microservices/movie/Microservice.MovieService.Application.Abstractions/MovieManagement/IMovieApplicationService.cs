namespace Microservice.MovieService.Application.MovieManagement;

public interface IMovieApplicationService
{
    public Task<MovieDto> GetAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<List<MovieDto>> GetListAsync(CancellationToken cancellationToken = default);

    public Task<MovieDto> CreateAsync(CreateMovieDto movie, CancellationToken cancellationToken = default);

    public Task<MovieDto> UpdateAsync(Guid id, UpdateMovieDto movie, CancellationToken cancellationToken = default);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

}