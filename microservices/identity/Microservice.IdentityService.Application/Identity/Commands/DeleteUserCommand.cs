using Microservice.Application;
using Microservice.Core;

namespace Microservice.IdentityService.Identity;

public class DeleteUserCommand : DeleteCommand<Guid>
{
    public DeleteUserCommand(Guid id) : base(id)
    {
    }

    public class DeleteUserCommandHandler : CommandHandler<DeleteUserCommand>
    {
        private readonly IUserManager _userManager;

        public DeleteUserCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        protected override async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.Id, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), request.Id);
            }

            var result = await _userManager.DeleteAsync(user, cancellationToken);
            result.CheckErrors();

            return Unit.Value;
        }
    }
}