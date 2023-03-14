using FluentValidation;
using FluentValidation.Results;
using Microservice.Core;
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
        private readonly IValidator<CreateUserDto> _validator;

        public CreateUserCommandHandler(IUserManager userManager, IValidator<CreateUserDto> validator)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));

            _userManager = userManager;
            _validator = validator;
        }

        protected override async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            User user = new(Guid.NewGuid(), model.Name, model.FirstName, model.LastName, model.Email);
            var result = await _userManager.CreateAsync(user, model.Password, cancellationToken);
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