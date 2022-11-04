using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class GetRoleByIdQuery : ItemQuery<Guid>
{
    public GetRoleByIdQuery(Guid id) : base(id)
    {
    }

    public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery>
    {
        private readonly IRoleManager _roleManager;

        public GetRoleByIdQueryHandler(IRoleManager roleManager)
        {
            ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));

            _roleManager = roleManager;
        }

        public async Task Consume(ConsumeContext<GetRoleByIdQuery> context)
        {
            Role role = await _roleManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (role == null)
            {
                throw new Exception($"Role (id = '{context.Message.Id}') not found");
            }

            await context.RespondAsync(new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            });
        }
    }
}