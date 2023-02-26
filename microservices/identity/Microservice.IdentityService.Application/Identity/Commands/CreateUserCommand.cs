using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class CreateUserCommand : CreateCommand<CreateUserDto, UserDto>
{
    public CreateUserCommand(CreateUserDto model) : base(model)
    {
    }

    public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserManager _userManager;

        public CreateUserCommandHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        protected override async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserDto userDto = request.Model;

            User user = new(Guid.NewGuid(), userDto.Name, userDto.FirstName, userDto.LastName, userDto.Email);
            var result = await _userManager.CreateAsync(user, userDto.Password, cancellationToken);
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