using FluentValidation;
using FluentValidation.Results;
using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class UpdateUserPasswordCommand : UpdateCommand<Guid, UpdateUserPasswordDto, Unit>
{
    public UpdateUserPasswordCommand(Guid id, UpdateUserPasswordDto model) : base(id, model)
    {
    }

    public class UpdateUserPasswordCommandHandler : CommandHandler<UpdateUserPasswordCommand, Unit>
    {
        private readonly IUserManager _userManager;
        private readonly IValidator<UpdateUserPasswordDto> _validator;

        public UpdateUserPasswordCommandHandler(IUserManager userManager, IValidator<UpdateUserPasswordDto> validator)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));

            _userManager = userManager;
            _validator = validator;
        }

        protected override async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            UpdateUserPasswordDto model = request.Model;
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

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password, cancellationToken);
            result.CheckErrors();

            return Unit.Value;
        }
    }
}