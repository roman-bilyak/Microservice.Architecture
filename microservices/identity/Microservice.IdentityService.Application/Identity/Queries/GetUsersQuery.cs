using Microservice.CQRS;
using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class GetUsersQuery : ListQuery<UserListDto>
{
    public GetUsersQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
    }

    public class GetUsersQueryHandler : QueryHandler<GetUsersQuery, UserListDto>
    {
        private readonly IReadRepository<User> _userRepository;

        public GetUsersQueryHandler(IReadRepository<User> userRepository)
        {
            ArgumentNullException.ThrowIfNull(userRepository, nameof(userRepository));

            _userRepository = userRepository;
        }

        protected override async Task<UserListDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            ISpecification<User> specification = new GetUsersSpecification().AsNoTracking();
            UserListDto result = new()
            {
                TotalCount = await _userRepository.CountAsync(specification, cancellationToken)
            };

            specification.ApplyPaging(request.PageIndex, request.PageSize);

            List<User> users = await _userRepository.ListAsync(specification, cancellationToken);
            foreach (User user in users)
            {
                result.Items.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsEmailConfirmed = user.IsEmailConfirmed
                });
            }

            return result;
        }
    }
}