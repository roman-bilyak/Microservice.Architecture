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
        public Task Consume(ConsumeContext<DeleteUserCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}