using Microservice.Application;
using Microservice.Core;

namespace Microservice.IdentityService.Identity;

public class GetRoleByIdQuery : ItemQuery<Guid, RoleDto>
{
    public GetRoleByIdQuery(Guid id) : base(id)
    {
    }

    public class GetRoleByIdQueryHandler : QueryHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRoleManager _roleManager;

        public GetRoleByIdQueryHandler(IRoleManager roleManager)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _roleManager = roleManager;
        }

        protected override async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            Role? role = await _roleManager.FindByIdAsync(request.Id, cancellationToken);
            if (role is null)
            {
                throw new EntityNotFoundException(typeof(Role), request.Id);
            }

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}