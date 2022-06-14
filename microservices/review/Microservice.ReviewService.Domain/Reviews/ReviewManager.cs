using Microservice.Core.Domain;
using Microservice.Core.Database;

namespace Microservice.ReviewService.Reviews;

internal class ReviewManager : DomainService, IReviewManager
{
    private readonly IRepository<Review> _reviewRepository;

    public ReviewManager(IRepository<Review> reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<Review> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _reviewRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<Review>> GetListByMovieAsync(Guid movieId, CancellationToken cancellationToken)
    {
        return await _reviewRepository.ListAsync(new MovieReviewSpecification(movieId), cancellationToken);
    }

    public async Task<List<Review>> GetListByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _reviewRepository.ListAsync(new UserReviewSpecification(userId), cancellationToken);
    }

    public async Task<Review> AddAsync(Review review, CancellationToken cancellationToken)
    {
        return await _reviewRepository.AddAsync(review, cancellationToken);
    }

    public async Task DeleteAsync(Review review, CancellationToken cancellationToken)
    {
        await _reviewRepository.DeleteAsync(review, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _reviewRepository.SaveChangesAsync(cancellationToken);
    }
}