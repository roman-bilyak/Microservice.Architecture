using Microservice.Application;
using Microservice.Core;
using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class GetUserRolesQuery : Query<UserRoleListDto>
{
    public Guid Id { get; protected set; }

    public GetUserRolesQuery(Guid id)
    {
        Id = id;
    }

    public class GetUserRolesQueryHandler : QueryHandler<GetUserRolesQuery, UserRoleListDto>
    {
        private readonly IUserManager _userManager;
        private readonly IReadRepository<Role> _roleRepository;

        public GetUserRolesQueryHandler
        (
            IUserManager userManager,
            IReadRepository<Role> roleRepository            
        )
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            ArgumentNullException.ThrowIfNull(roleRepository, nameof(roleRepository));

            _userManager = userManager;
            _roleRepository = roleRepository;
        }

        protected override async Task<UserRoleListDto> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.Id, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), request.Id);
            }

            List<Guid> roleIds = user.Roles.Select(x => x.RoleId).ToList();
            ISpecification<Role> specification = new GetRolesSpecification(roleIds).AsNoTracking();
            List<Role> roles = await _roleRepository.ListAsync(specification, cancellationToken);

            UserRoleListDto result = new()
            {
                TotalCount = roles.Count
            };

            foreach (Role role in roles)
            {
                result.Items.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }

            return result;
        }
    }
}
