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
        public Task Consume(ConsumeContext<GetRoleByIdQuery> context)
        {
            throw new NotImplementedException();
        }
    }
}