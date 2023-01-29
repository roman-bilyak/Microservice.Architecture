using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class DeleteUserCommand : DeleteCommand<Guid>
{
    public DeleteUserCommand(Guid id) : base(id)
    {
    }

    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserManager _userManager;

        public DeleteUserCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<DeleteUserCommand> context)
        {
            User? user = await _userManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (user is null)
            {
                throw new Exception($"User (id = '{context.Message.Id}') not found");
            }

            var result = await _userManager.DeleteAsync(user, context.CancellationToken);
            result.CheckErrors();
        }
    }
}