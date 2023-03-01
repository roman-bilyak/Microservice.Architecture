using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class UpdateUserCommand : UpdateCommand<Guid, UpdateUserDto, UserDto>
{
    public UpdateUserCommand(Guid id, UpdateUserDto model) : base(id, model)
    {
    }

    public class UpdateUserCommandHandler : CommandHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserManager _userManager;

        public UpdateUserCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        protected override async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.Id, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), request.Id);
            }

            UpdateUserDto userDto = request.Model;
            user.SetName(userDto.Name);
            user.SetFirstName(userDto.FirstName);
            user.SetLastName(userDto.LastName);
            user.SetEmail(userDto.Email);

            var result = await _userManager.UpdateAsync(user, cancellationToken);
            result.CheckErrors();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsEmailConfirmed = user.IsEmailConfirmed
            };
        }
    }
}