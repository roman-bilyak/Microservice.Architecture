using FluentValidation;
using FluentValidation.Results;
using Microservice.Application;
using Microservice.Core;

namespace Microservice.ReviewService.Reviews;

public class CreateReviewCommand : CreateCommand<CreateReviewDto, ReviewDto>
{
    public Guid MovieId { get; protected set; }

    public CreateReviewCommand(Guid movieId, CreateReviewDto model) : base(model)
    {
        MovieId = movieId;
    }

    public class CreateReviewCommandHandler : CommandHandler<CreateReviewCommand, ReviewDto>
    {
        private readonly IReviewManager _reviewManager;
        private readonly IValidator<CreateReviewDto> _validator;
        private readonly ICurrentUser _currentUser;

        public CreateReviewCommandHandler
        (
            IReviewManager reviewManager, 
            IValidator<CreateReviewDto> validator, 
            ICurrentUser currentUser
        )
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));
            ArgumentNullException.ThrowIfNull(currentUser, nameof(_currentUser));

            _reviewManager = reviewManager;
            _validator = validator;
            _currentUser = currentUser;
        }

        protected override async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            CreateReviewDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            Guid userId = _currentUser.Id.Value;
            Review review = new(Guid.NewGuid(), request.MovieId, userId, model.Comment, model.Rating);

            review = await _reviewManager.AddAsync(review, cancellationToken);
            await _reviewManager.SaveChangesAsync(cancellationToken);

            return new ReviewDto
            {
                Id = review.Id,
                MovieId = review.MovieId,
                UserId = review.UserId,
                Comment = review.Comment,
                Rating = review.Rating
            };
        }
    }
}