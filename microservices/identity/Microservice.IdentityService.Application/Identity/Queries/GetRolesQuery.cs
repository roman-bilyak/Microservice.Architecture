using MassTransit;
using Microservice.CQRS;
using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class GetRolesQuery : ListQuery
{
    public GetRolesQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
    }

    public class GetRolesQueryHandler : IQueryHandler<GetRolesQuery>
    {
        private readonly IReadRepository<Role> _roleRepository;

        public GetRolesQueryHandler(IReadRepository<Role> roleRepository)
        {
            ArgumentNullException.ThrowIfNull(roleRepository, nameof(roleRepository));

            _roleRepository = roleRepository;
        }

        public async Task Consume(ConsumeContext<GetRolesQuery> context)
        {
            RoleListDto result = new RoleListDto
            {
                TotalCount = await _roleRepository.CountAsync(context.CancellationToken)
            };

            Specification<Role> specification = new Specification<Role>();
            specification.ApplyPaging(context.Message.PageIndex, context.Message.PageSize);

            List<Role> roles = await _roleRepository.ListAsync(specification, context.CancellationToken);
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