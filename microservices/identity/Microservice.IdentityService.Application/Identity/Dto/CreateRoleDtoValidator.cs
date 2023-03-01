using FluentValidation;

namespace Microservice.IdentityService.Identity;

internal class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Role.MaxNameLength);
    }
}