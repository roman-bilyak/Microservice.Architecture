using MassTransit;
using Microservice.Core;
using Microservice.CQRS;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityService.Identity;

public class UpdateRoleCommand : UpdateCommand<Guid, UpdateRoleDto>
{
    public UpdateRoleCommand(Guid id, UpdateRoleDto model) : base(id, model)
    {
    }

    public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand>
    {
        private readonly IRoleManager _roleManager;

        public UpdateRoleCommandHandler(IRoleManager roleManager)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _roleManager = roleManager;
        }

        public async Task Consume(ConsumeContext<UpdateRoleCommand> context)
        {
            Role? role = await _roleManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (role is null)
            {
                throw new EntityNotFoundException(typeof(Role), context.Message.Id);
            }

            UpdateRoleDto roleDto = context.Message.Model;
            role.SetName(roleDto.Name);

            IdentityResult result = await _roleManager.UpdateAsync(role, context.CancellationToken);
            result.CheckErrors();

            context.Respond(new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            });
        }
    }
}