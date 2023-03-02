using FluentValidation;

namespace Microservice.ReviewService.Reviews;

internal class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewDtoValidator()
    {
        RuleFor(x => x.Comment)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Review.MaxCommentLength);
    }
}