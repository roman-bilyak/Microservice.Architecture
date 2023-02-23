using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class DeleteRoleCommand : DeleteCommand<Guid>
{
    public DeleteRoleCommand(Guid id) : base(id)
    {
    }

    public class DeleteRoleCommandHandler : CommandHandler<DeleteRoleCommand>
    {
        private readonly IRoleManager _roleManager;

        public DeleteRoleCommandHandler(IRoleManager roleManager)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _roleManager = roleManager;
        }

        protected override async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            Role? role = await _roleManager.FindByIdAsync(request.Id, cancellationToken);
            if (role is null)
            {
                throw new EntityNotFoundException(typeof(Role), request.Id);
            }

            var result = await _roleManager.DeleteAsync(role, cancellationToken);
            result.CheckErrors();

            return Unit.Value;
        }
    }
}