using MassTransit;
using Microservice.Core;
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
            User? user = await _userManager.FindByIdAsync(context.Message.UserId, context.CancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), context.Message.UserId);
            }

            var result = await _userManager.RemoveFromRoleAsync(user, context.Message.RoleName, context.CancellationToken);
            result.CheckErrors();
        }
    }
}