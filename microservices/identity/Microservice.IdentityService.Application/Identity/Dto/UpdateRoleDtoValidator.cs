using FluentValidation;
using Microservice.IdentityService.Identity;

namespace Microservice.IdentityService;

internal class UpdateRoleDtoValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Role.MaxNameLength);
    }
}