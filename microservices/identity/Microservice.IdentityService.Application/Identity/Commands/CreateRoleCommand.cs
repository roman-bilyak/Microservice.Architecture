using FluentValidation;
using FluentValidation.Results;
using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class CreateRoleCommand : CreateCommand<CreateRoleDto, RoleDto>
{
    public CreateRoleCommand(CreateRoleDto model) : base(model)
    {
    }

    public class CreateRoleCommandHandler : CommandHandler<CreateRoleCommand, RoleDto>
    {
        private readonly IRoleManager _roleManager;
        private readonly IValidator<CreateRoleDto> _validator;

        public CreateRoleCommandHandler(IRoleManager roleManager, IValidator<CreateRoleDto> validator)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));

            _roleManager = roleManager;
            _validator = validator;
        }

        protected override async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            CreateRoleDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            Role role = new(Guid.NewGuid(), model.Name);
            var result = await _roleManager.CreateAsync(role, cancellationToken);
            result.CheckErrors();

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}