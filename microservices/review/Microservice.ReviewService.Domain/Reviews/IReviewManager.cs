namespace Microservice.ReviewService.Reviews;

public interface IReviewManager
{
    Task<Review?> GetByIdAsync(Guid movieId, Guid reviewId, CancellationToken cancellationToken);

    Task<List<Review>> GetListByMovieAsync(Guid movieId, int pageIndex, int pageSize, CancellationToken cancellationToken);

    Task<int> GetCountByMovieAsync(Guid movieId, CancellationToken cancellationToken);

    Task<List<Review>> GetListByUserAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);

    Task<int> GetCountByUserAsync(Guid userId, CancellationToken cancellationToken);

    Task<Review> AddAsync(Review review, CancellationToken cancellationToken);

    Task<Review> UpdateAsync(Review review, CancellationToken cancellationToken);

    Task DeleteAsync(Review review, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}