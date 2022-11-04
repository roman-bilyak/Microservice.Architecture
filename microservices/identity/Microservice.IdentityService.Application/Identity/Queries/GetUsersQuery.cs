using MassTransit;
using Microservice.CQRS;
using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class GetUsersQuery : ListQuery
{
    public GetUsersQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
    }

    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery>
    {
        private readonly IReadRepository<User> _userRepository;

        public GetUsersQueryHandler(IReadRepository<User> userRepository)
        {
            ArgumentNullException.ThrowIfNull(userRepository, nameof(userRepository));

            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<GetUsersQuery> context)
        {
            UserListDto result = new UserListDto
            {
                TotalCount = await _userRepository.CountAsync(context.CancellationToken)
            };

            Specification<User> specification = new Specification<User>();
            specification.ApplyPaging(context.Message.PageIndex, context.Message.PageSize);

            List<User> users = await _userRepository.ListAsync(specification, context.CancellationToken);
            foreach (User user in users)
            {
                result.Items.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }

            await context.RespondAsync(result);
        }
    }
}