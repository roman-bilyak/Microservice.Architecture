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
        private readonly IUserManager _userManager;

        public CreateUserCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<CreateUserCommand> context)
        {
            CreateUserDto userDto = context.Message.Model;

            User user = new User(Guid.NewGuid(), userDto.Name, userDto.FirstName, userDto.LastName, userDto.Email);
            var result = await _userManager.CreateAsync(user, userDto.Password, context.CancellationToken);
            result.CheckErrors();

            context.Respond(new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }
    }
}