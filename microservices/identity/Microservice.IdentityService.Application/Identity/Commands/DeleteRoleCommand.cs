using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class DeleteRoleCommand : DeleteCommand<Guid>
{
    public DeleteRoleCommand(Guid id) : base(id)
    {
    }

    public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
    {
        public Task Consume(ConsumeContext<DeleteRoleCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}