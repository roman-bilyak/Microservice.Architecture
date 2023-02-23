using Microservice.Core;
using Microservice.CQRS;
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

        public UpdateRoleCommandHandler(IRoleManager roleManager)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _roleManager = roleManager;
        }

        protected override async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Role? role = await _roleManager.FindByIdAsync(request.Id, cancellationToken);
            if (role is null)
            {
                throw new EntityNotFoundException(typeof(Role), request.Id);
            }

            UpdateRoleDto roleDto = request.Model;
            role.SetName(roleDto.Name);

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