using FluentValidation;

namespace Microservice.IdentityService.Identity;

internal class UpdateUserPasswordDtoValidator : AbstractValidator<UpdateUserPasswordDto>
{
    public UpdateUserPasswordDtoValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .MaximumLength(User.MaxPasswordLength);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(User.MaxPasswordLength);
    }
}