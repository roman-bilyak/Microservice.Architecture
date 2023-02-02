using MassTransit;
using Microservice.Core;
using Microservice.CQRS;
using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class GetUserRolesQuery : Query
{
    public Guid Id { get; protected set; }

    public GetUserRolesQuery(Guid id)
    {
        Id = id;
    }

    public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery>
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

        public async Task Consume(ConsumeContext<GetUserRolesQuery> context)
        {
            User? user = await _userManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), context.Message.Id);
            }

            List<Guid> roleIds = user.Roles.Select(x => x.RoleId).ToList();
            GetRolesByIdsSpecification specification = new GetRolesByIdsSpecification(roleIds);
            List<Role> roles = await _roleRepository.ListAsync(specification, context.CancellationToken);

            UserRoleListDto result = new UserRoleListDto
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

            await context.RespondAsync(result);
        }
    }
}
