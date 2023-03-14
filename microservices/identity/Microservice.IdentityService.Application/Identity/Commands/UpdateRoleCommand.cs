using FluentValidation;
using FluentValidation.Results;
using Microservice.Application;
using Microservice.Core;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityService.Identity;

public class UpdateRoleCommand : UpdateCommand<Guid, UpdateRoleDto, RoleDto>
{
    public UpdateRoleCommand(Guid id, UpdateRoleDto model) : base(id, model)
    {
    }

    public class UpdateRoleCommandHandler : CommandHandler<UpdateRoleCommand, RoleDto>
    {
        private readonly IRoleManager _roleManager;
        private readonly IValidator<UpdateRoleDto> _validator;

        public UpdateRoleCommandHandler(IRoleManager roleManager, IValidator<UpdateRoleDto> validator)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));

            _roleManager = roleManager;
            _validator = validator;
        }

        protected override async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            UpdateRoleDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            Role? role = await _roleManager.FindByIdAsync(request.Id, cancellationToken);
            if (role is null)
            {
                throw new EntityNotFoundException(typeof(Role), request.Id);
            }

            role.SetName(model.Name);

            IdentityResult result = await _roleManager.UpdateAsync(role, cancellationToken);
            result.CheckErrors();

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}