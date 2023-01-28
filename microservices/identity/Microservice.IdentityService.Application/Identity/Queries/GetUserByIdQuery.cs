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
        private readonly IUserManager _userManager;

        public GetUserByIdQueryHandler(IUserManager userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<GetUserByIdQuery> context)
        {
            User user = await _userManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (user == null)
            {
                throw new Exception($"User (id = '{context.Message.Id}') not found");
            }

            await context.RespondAsync(new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsEmilConfirmed = user.IsEmailConfirmed
            });
        }
    }
}