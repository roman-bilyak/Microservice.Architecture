using FluentValidation;

namespace Microservice.IdentityService.Identity;

internal class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Role.MaxNameLength);
    }
}