using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class UpdateUserCommand : UpdateCommand<Guid, UpdateUserDto>
{
    public UpdateUserCommand(Guid id, UpdateUserDto model) : base(id, model)
    {
    }

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        public Task Consume(ConsumeContext<UpdateUserCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}