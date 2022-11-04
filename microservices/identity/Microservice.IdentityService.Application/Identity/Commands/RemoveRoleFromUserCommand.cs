using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class RemoveUserFromRoleCommand : Command
{
    public Guid UserId { get; protected set; }

    public string RoleName { get; protected set; }

    public RemoveUserFromRoleCommand(Guid userId, string roleName)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));
        ArgumentNullException.ThrowIfNull(roleName, nameof(roleName));

        UserId = userId;
        RoleName = roleName;
    }

    public class RemoveUserFromRoleCommandHandler : ICommandHandler<RemoveUserFromRoleCommand>
    {
        private readonly IUserManager _userManager;

        public RemoveUserFromRoleCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<RemoveUserFromRoleCommand> context)
        {
            User user = await _userManager.FindByIdAsync(context.Message.UserId, context.CancellationToken);
            if (user == null)
            {
                throw new Exception($"User (id = '{context.Message.UserId}') not found");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, context.Message.RoleName, context.CancellationToken);
            result.CheckErrors();
        }
    }
}