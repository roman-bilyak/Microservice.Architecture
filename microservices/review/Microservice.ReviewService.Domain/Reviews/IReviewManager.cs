namespace Microservice.ReviewService.Reviews;

public interface IReviewManager
{
    Task<Review> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Review>> GetListByMovieAsync(Guid movieId, CancellationToken cancellationToken);

    Task<List<Review>> GetListByUserAsync(Guid userId, CancellationToken cancellationToken);

    Task<Review> AddAsync(Review review, CancellationToken cancellationToken);

    Task DeleteAsync(Review review, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}