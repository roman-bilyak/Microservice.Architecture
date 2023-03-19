using FluentValidation;
using FluentValidation.Results;
using Microservice.Application;
using Microservice.Core;

namespace Microservice.ReviewService.Reviews;

public class UpdateReviewCommand : UpdateCommand<Guid, UpdateReviewDto, ReviewDto>
{
    public Guid MovieId { get; protected set; }

    public UpdateReviewCommand(Guid movieId, Guid id, UpdateReviewDto model) : base(id, model)
    {
        MovieId = movieId;
    }

    public class UpdateMovieCommandHandler : CommandHandler<UpdateReviewCommand, ReviewDto>
    {
        private readonly IReviewManager _reviewManager;
        private readonly IValidator<UpdateReviewDto> _validator;
        private readonly ICurrentUser _currentUser;

        public UpdateMovieCommandHandler
        (
            IReviewManager reviewManager,
            IValidator<UpdateReviewDto> validator,
            ICurrentUser currentUser
        )
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));
            ArgumentNullException.ThrowIfNull(currentUser, nameof(currentUser));

            _reviewManager = reviewManager;
            _validator = validator;
            _currentUser = currentUser;
        }

        protected override async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            UpdateReviewDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            Review? review = await _reviewManager.GetByIdAsync(request.MovieId, request.Id, cancellationToken);
            if (review is null || review.UserId != _currentUser.Id)
            {
                throw new EntityNotFoundException(typeof(Review), request.Id);
            }

            review.Update(model.Comment, model.Rating);

            review = await _reviewManager.UpdateAsync(review, cancellationToken);
            await _reviewManager.SaveChangesAsync(cancellationToken);

            return new ReviewDto
            {
                Id = review.Id,
                MovieId = review.MovieId,
                UserId = review.UserId,
                Comment = review.Comment,
                Rating = review.Rating,
            };
        }
    }
}