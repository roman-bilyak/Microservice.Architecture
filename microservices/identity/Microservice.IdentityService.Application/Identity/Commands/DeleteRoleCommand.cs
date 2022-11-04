using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class DeleteRoleCommand : DeleteCommand<Guid>
{
    public DeleteRoleCommand(Guid id) : base(id)
    {
    }

    public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
    {
        private readonly IRoleManager _roleManager;

        public DeleteRoleCommandHandler(IRoleManager roleManager)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _roleManager = roleManager;
        }

        public async Task Consume(ConsumeContext<DeleteRoleCommand> context)
        {
            Role role = await _roleManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (role == null)
            {
                throw new Exception($"Role (id = '{context.Message.Id}') not found");
            }

            var result = await _roleManager.DeleteAsync(role, context.CancellationToken);
            result.CheckErrors();
        }
    }
}