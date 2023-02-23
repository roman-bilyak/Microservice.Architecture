using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.IdentityService.Identity;

public class GetUserByIdQuery : ItemQuery<Guid, UserDto>
{
    public GetUserByIdQuery(Guid id) : base(id)
    {
    }

    public class GetUserByIdQueryHandler : QueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserManager _userManager;

        public GetUserByIdQueryHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        protected override async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.Id, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(User), request.Id);
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsEmilConfirmed = user.IsEmailConfirmed
            };
        }
    }
}