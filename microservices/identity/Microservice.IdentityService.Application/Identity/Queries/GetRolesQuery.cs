using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class GetRolesQuery : ListQuery
{
    public GetRolesQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
    }

    public class GetRolesQueryHandler : IQueryHandler<GetRolesQuery>
    {
        public Task Consume(ConsumeContext<GetRolesQuery> context)
        {
            throw new NotImplementedException();
        }
    }
}