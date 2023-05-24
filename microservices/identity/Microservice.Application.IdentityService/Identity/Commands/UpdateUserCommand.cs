using FluentValidation;
using FluentValidation.Results;
using Microservice.Application;
using Microservice.Core;

namespace Microservice.IdentityService.Identity;

public class UpdateUserCommand : UpdateCommand<Guid, UpdateUserDto, UserDto>
{
    public UpdateUserCommand(Guid id, UpdateUserDto model) : base(id, model)
    {
    }

    public class UpdateUserCommandHandler : CommandHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserManager _userManager;
        private readonly IValidator<UpdateUserDto> _validator;

        public UpdateUserCommandHandler(IUserManager userManager, IValidator<UpdateUserDto> validator)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));

            _userManager = userManager;
            _validator = validator;
        }

        protected override async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            UpdateUserDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            User? user = await _userManager.FindByIdAsync(request.Id, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), request.Id);
            }

            user.SetName(model.Name);
            user.SetFirstName(model.FirstName);
            user.SetLastName(model.LastName);
            user.SetEmail(model.Email);

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