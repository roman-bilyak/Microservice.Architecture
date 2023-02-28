using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class AddUserToRoleCommand : Command<Unit>
{
    public Guid UserId { get; protected set; }

    public Guid RoleId { get; protected set; }

    public AddUserToRoleCommand(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public class AddRoleToUserCommandHandler : CommandHandler<AddUserToRoleCommand, Unit>
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;

        public AddRoleToUserCommandHandler
        (
            IUserManager userManager,
            IRoleManager roleManager
        )
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task<Unit> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), request.UserId);
            }

            Role? role = await _roleManager.FindByIdAsync(request.RoleId, cancellationToken);
            if (role is null)
            {
                throw new EntityNotFoundException(typeof(Role), request.RoleId);
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name, cancellationToken);
            result.CheckErrors();

            return Unit.Value;
        }
    }
}