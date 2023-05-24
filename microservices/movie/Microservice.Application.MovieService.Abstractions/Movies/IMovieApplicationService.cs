using Microservice.Application;

namespace Microservice.MovieService.Movies;

/// <summary>
/// Provides methods for managing movies in the application.
/// </summary>
public interface IMovieApplicationService : IApplicationService
{
    /// <summary>
    /// Retrieves a paginated list of movies based on the provided page index and page size.
    /// </summary>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A paginated list of movies.</returns>
    public Task<MovieListDto> GetListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the details of a movie based on the provided movie id.
    /// </summary>
    /// <param name="movieId">The id of the movie to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the movie.</returns>
    public Task<MovieDto> GetAsync(Guid movieId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new movie based on the provided movie data.
    /// </summary>
    /// <param name="movie">The data for the new movie.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the newly created movie.</returns>
    public Task<MovieDto> CreateAsync(CreateMovieDto movie, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the details of an existing movie based on the provided movie id and movie data.
    /// </summary>
    /// <param name="movieId">The id of the movie to update.</param>
    /// <param name="movie">The updated data for the movie.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the updated movie.</returns>
    public Task<MovieDto> UpdateAsync(Guid movieId, UpdateMovieDto movie, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an existing movie based on the provided movie id.
    /// </summary>
    /// <param name="movieId">The id of the movie to delete.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    public Task DeleteAsync(Guid movieId, CancellationToken cancellationToken = default);
}