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

        public CreateRoleCommandHandler(IRoleManager roleManager)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _roleManager = roleManager;
        }

        protected override async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            CreateRoleDto roleDto = request.Model;

            Role role = new(Guid.NewGuid(), roleDto.Name);
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