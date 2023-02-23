using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class UpdateUserPasswordCommand : Command<Unit>
{
    public Guid UserId { get; protected set; }

    public string OldPassword { get; protected set; }

    public string Password { get; protected set; }

    public UpdateUserPasswordCommand(Guid userId, string oldPassword, string password)
    {
        ArgumentNullException.ThrowIfNull(oldPassword, nameof(oldPassword));
        ArgumentNullException.ThrowIfNull(password, nameof(password));

        UserId = userId;
        OldPassword = oldPassword;
        Password = password;
    }

    public class UpdateUserPasswordCommandHandler : CommandHandler<UpdateUserPasswordCommand, Unit>
    {
        private readonly IUserManager _userManager;

        public UpdateUserPasswordCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        protected override async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), request.UserId);
            }

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.Password, cancellationToken);
            result.CheckErrors();

            return Unit.Value;
        }
    }
}