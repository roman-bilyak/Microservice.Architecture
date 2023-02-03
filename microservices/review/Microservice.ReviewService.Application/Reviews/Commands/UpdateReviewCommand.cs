using MassTransit;
using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.ReviewService.Reviews;

public class UpdateReviewCommand : UpdateCommand<Guid, UpdateReviewDto>
{
    public Guid MovieId { get; protected set; }

    public UpdateReviewCommand(Guid movieId, Guid id, UpdateReviewDto model) : base(id, model)
    {
        MovieId = movieId;
    }

    public class UpdateMovieCommandHandler : ICommandHandler<UpdateReviewCommand>
    {
        private readonly IReviewManager _reviewManager;

        public UpdateMovieCommandHandler(IReviewManager reviewManager)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));

            _reviewManager = reviewManager;
        }

        public async Task Consume(ConsumeContext<UpdateReviewCommand> context)
        {
            Review? review = await _reviewManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (review is null || review.MovieId != context.Message.MovieId)
            {
                throw new EntityNotFoundException(typeof(Review), context.Message.Id);
            }

            review.Update(context.Message.Model.Text, context.Message.Model.Rating);

            review = await _reviewManager.UpdateAsync(review, context.CancellationToken);
            await _reviewManager.SaveChangesAsync(context.CancellationToken);

            await context.RespondAsync(new ReviewDto
            {
                Id = review.Id,
                UserId = review.UserId,
                MovieId = review.MovieId,
                Text = review.Text,
                Rating = review.Rating,
            });
        }
    }
}