using MassTransit;
using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class UpdateUserCommand : UpdateCommand<Guid, UpdateUserDto>
{
    public UpdateUserCommand(Guid id, UpdateUserDto model) : base(id, model)
    {
    }

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserManager _userManager;

        public UpdateUserCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<UpdateUserCommand> context)
        {
            User? user = await _userManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), context.Message.Id);
            }

            UpdateUserDto userDto = context.Message.Model;
            user.SetName(userDto.Name);
            user.SetFirstName(userDto.FirstName);
            user.SetLastName(userDto.LastName);
            user.SetEmail(userDto.Email);

            var result = await _userManager.UpdateAsync(user, context.CancellationToken);
            result.CheckErrors();

            context.Respond(new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsEmilConfirmed = user.IsEmailConfirmed
            });
        }
    }
}