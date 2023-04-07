using FluentValidation;

namespace Microservice.IdentityService.Identity;

internal class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(User.MaxNameLength);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(User.MaxFirstNameLength);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(User.MaxLastNameLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(User.MaxEmailLength);
    }
}