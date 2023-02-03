using FluentValidation;

namespace Microservice.ReviewService.Reviews;

internal class CreateReviewDtoValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(Review.MaxTextLength);
    }
}
