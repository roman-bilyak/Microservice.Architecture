using Microservice.Database;

namespace Microservice.ReviewService.Reviews;

public class Review : Entity<Guid>, IAggregateRoot
{
    public const int MaxCommentLength = 500;

    public Guid MovieId { get; protected set; }

    public Guid UserId { get; protected set; }

    public string Comment { get; protected set; } = string.Empty;

    public RatingEnum Rating { get; protected set; }

    protected Review()
    {

    }

    public Review
    (
        Guid id,
        Guid movieId,
        Guid userId,
        string comment,
        RatingEnum rating
    ) : base(id)
    {
        ArgumentNullException.ThrowIfNull(comment, nameof(comment));

        MovieId = movieId;
        UserId = userId;
        Comment = comment;
        Rating = rating;
    }

    public void Update(string comment, RatingEnum rating)
    {
        ArgumentNullException.ThrowIfNull(comment, nameof(comment));

        Comment = comment;
        Rating = rating;
    }
}