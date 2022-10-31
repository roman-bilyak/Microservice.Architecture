using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class CreateUserCommand : CreateCommand<CreateUserDto>
{
    public CreateUserCommand(CreateUserDto model) : base(model)
    {
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public Task Consume(ConsumeContext<CreateUserCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}