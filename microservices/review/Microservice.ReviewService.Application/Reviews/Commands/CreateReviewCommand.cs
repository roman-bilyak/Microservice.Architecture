using FluentValidation;
using FluentValidation.Results;
using Microservice.Core;
using Microservice.CQRS;

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

        public CreateReviewCommandHandler(IReviewManager reviewManager, IValidator<CreateReviewDto> validator)
        {
            ArgumentNullException.ThrowIfNull(reviewManager, nameof(reviewManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));

            _reviewManager = reviewManager;
            _validator = validator;
        }

        protected override async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            CreateReviewDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            Guid userId = Guid.Empty; //TODO: use current user id
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