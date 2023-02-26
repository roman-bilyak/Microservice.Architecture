using Microservice.Application;

namespace Microservice.ReviewService.Reviews;

/// <summary>
/// Provides methods for managing reviews for a movie in the application.
/// </summary>
public interface IMovieApplicationService : IApplicationService
{
    /// <summary>
    /// Retrieves a paginated list of reviews for a movie based on the provided movie id, page index, and page size.
    /// </summary>
    /// <param name="movieId">The id of the movie for which to retrieve reviews.</param>
    /// <param name="pageIndex">The index of the page to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A paginated list of reviews for the movie.</returns>
    public Task<MovieReviewListDto> GetReviewListAsync(Guid movieId, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the details of a review for a movie based on the provided movie id and review id.
    /// </summary>
    /// <param name="movieId">The id of the movie for which to retrieve the review.</param>
    /// <param name="reviewId">The id of the review to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the review.</returns>
    Task<ReviewDto> GetReviewAsync(Guid movieId, Guid reviewId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new review for a movie based on the provided movie id and review data.
    /// </summary>
    /// <param name="movieId">The id of the movie for which to create a review.</param>
    /// <param name="review">The data for the new review.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the newly created review.</returns>
    Task<ReviewDto> CreateReviewAsync(Guid movieId, CreateReviewDto review, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the details of an existing review for a movie based on the provided movie id, review id, and review data.
    /// </summary>
    /// <param name="movieId">The id of the movie for which to update the review.</param>
    /// <param name="reviewId">The id of the review to update.</param>
    /// <param name="review">The updated data for the review.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The details of the updated review.</returns>
    Task<ReviewDto> UpdateReviewAsync(Guid movieId, Guid reviewId, UpdateReviewDto review, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an existing review for a movie based on the provided movie id and review id.
    /// </summary>
    /// <param name="movieId">The id of the movie for which to delete the review.</param>
    /// <param name="reviewId">The id of the review to delete.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    Task DeleteReviewAsync(Guid movieId, Guid reviewId, CancellationToken cancellationToken = default);
}