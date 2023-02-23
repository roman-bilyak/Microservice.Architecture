using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class RemoveUserFromRoleCommand : Command<Unit>
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

    public class RemoveUserFromRoleCommandHandler : CommandHandler<RemoveUserFromRoleCommand, Unit>
    {
        private readonly IUserManager _userManager;

        public RemoveUserFromRoleCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        protected override async Task<Unit> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), request.UserId);
            }

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName, cancellationToken);
            result.CheckErrors();

            return Unit.Value;
        }
    }
}