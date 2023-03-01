using Microservice.CQRS;
using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class GetRolesQuery : ListQuery<RoleListDto>
{
    public GetRolesQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
    }

    public class GetRolesQueryHandler : QueryHandler<GetRolesQuery, RoleListDto>
    {
        private readonly IReadRepository<Role> _roleRepository;

        public GetRolesQueryHandler(IReadRepository<Role> roleRepository)
        {
            ArgumentNullException.ThrowIfNull(roleRepository, nameof(roleRepository));

            _roleRepository = roleRepository;
        }

        protected override async Task<RoleListDto> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            RoleListDto result = new()
            {
                TotalCount = await _roleRepository.CountAsync(cancellationToken)
            };

            Specification<Role> specification = new();
            specification.ApplyPaging(request.PageIndex, request.PageSize);

            List<Role> roles = await _roleRepository.ListAsync(specification, cancellationToken);
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