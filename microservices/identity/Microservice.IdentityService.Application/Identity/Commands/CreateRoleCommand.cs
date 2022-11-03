using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class CreateRoleCommand : CreateCommand<CreateRoleDto>
{
    public CreateRoleCommand(CreateRoleDto model) : base(model)
    {
    }

    public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand>
    {
        private readonly IRoleManager _roleManager;

        public CreateRoleCommandHandler(IRoleManager roleManager)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _roleManager = roleManager;
        }

        public async Task Consume(ConsumeContext<CreateRoleCommand> context)
        {
            CreateRoleDto roleDto = context.Message.Model;

            Role role = new Role(Guid.NewGuid(), roleDto.Name);
            var result = await _roleManager.CreateAsync(role, context.CancellationToken);
            result.CheckErrors();

            context.Respond(new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            });
        }
    }
}