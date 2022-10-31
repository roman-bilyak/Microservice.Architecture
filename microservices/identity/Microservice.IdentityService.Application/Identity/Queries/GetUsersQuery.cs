using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class GetUsersQuery : ListQuery
{
    public GetUsersQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
    }

    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery>
    {
        public Task Consume(ConsumeContext<GetUsersQuery> context)
        {
            throw new NotImplementedException();
        }
    }
}