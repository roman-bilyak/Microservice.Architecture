using FluentValidation;

namespace Microservice.ReviewService.Reviews;

internal class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(Review.MaxTextLength);
    }
}