using Microservice.Database;

namespace Microservice.ReviewService.Reviews;

internal class ReviewManager : DomainService, IReviewManager
{
    private readonly IRepository<Review> _reviewRepository;

    public ReviewManager(IRepository<Review> reviewRepository)
    {
        ArgumentNullException.ThrowIfNull(reviewRepository, nameof(reviewRepository));

        _reviewRepository = reviewRepository;
    }

    public async Task<Review?> GetByIdAsync(Guid movieId, Guid reviewId, CancellationToken cancellationToken)
    {
        GetReviewsByMovieSpecification specification = new(movieId, reviewId);
        return await _reviewRepository.SingleOrDefaultAsync(specification, cancellationToken);
    }

    public async Task<List<Review>> GetListByMovieAsync(Guid movieId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        ISpecification<Review> specification = new GetReviewsByMovieSpecification(movieId)
            .ApplyPaging(pageIndex, pageSize)
            .AsNoTracking();
        return await _reviewRepository.ListAsync(specification, cancellationToken);
    }

    public async Task<int> GetCountByMovieAsync(Guid movieId, CancellationToken cancellationToken)
    {
        ISpecification<Review> specification = new GetReviewsByMovieSpecification(movieId).AsNoTracking();
        return await _reviewRepository.CountAsync(specification, cancellationToken);
    }

    public async Task<List<Review>> GetListByUserAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        ISpecification<Review> specification = new GetReviewsByUserSpecification(userId)
            .ApplyPaging(pageIndex, pageSize)
            .AsNoTracking();
        return await _reviewRepository.ListAsync(specification, cancellationToken);
    }

    public async Task<int> GetCountByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        ISpecification<Review> specification = new GetReviewsByUserSpecification(userId).AsNoTracking();
        return await _reviewRepository.CountAsync(specification, cancellationToken);
    }

    public async Task<Review> AddAsync(Review review, CancellationToken cancellationToken)
    {
        return await _reviewRepository.AddAsync(review, cancellationToken);
    }

    public async Task<Review> UpdateAsync(Review review, CancellationToken cancellationToken)
    {
        return await _reviewRepository.UpdateAsync(review, cancellationToken);
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