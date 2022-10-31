using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class CreateRoleCommand : CreateCommand<CreateRoleDto>
{
    public CreateRoleCommand(CreateRoleDto model) : base(model)
    {
    }

    public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand>
    {
        public Task Consume(ConsumeContext<CreateRoleCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}