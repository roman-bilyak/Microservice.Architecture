using MassTransit;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class GetUserByIdQuery : ItemQuery<Guid>
{
    public GetUserByIdQuery(Guid id) : base(id)
    {
    }

    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery>
    {
        public Task Consume(ConsumeContext<GetUserByIdQuery> context)
        {
            throw new NotImplementedException();
        }
    }
}