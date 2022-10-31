using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class UpdateRoleCommand : UpdateCommand<Guid, UpdateRoleDto>
{
    public UpdateRoleCommand(Guid id, UpdateRoleDto model) : base(id, model)
    {
    }

    public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand>
    {
        public Task Consume(ConsumeContext<UpdateRoleCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}