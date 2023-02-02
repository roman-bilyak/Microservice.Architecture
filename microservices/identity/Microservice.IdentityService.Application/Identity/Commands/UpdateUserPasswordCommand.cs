using MassTransit;
using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class UpdateUserPasswordCommand : Command
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

    public class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand>
    {
        private readonly IUserManager _userManager;

        public UpdateUserPasswordCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<UpdateUserPasswordCommand> context)
        {
            User? user = await _userManager.FindByIdAsync(context.Message.UserId, context.CancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), context.Message.UserId);
            }

            var result = await _userManager.ChangePasswordAsync(user, context.Message.OldPassword, context.Message.Password, context.CancellationToken);
            result.CheckErrors();
        }
    }
}
