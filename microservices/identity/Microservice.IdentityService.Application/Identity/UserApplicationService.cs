using MassTransit;
using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.IdentityService.Identity;

[Authorize]
internal class UserApplicationService : ApplicationService, IUserApplicationService
{
    private readonly IMediator _mediator;

    public UserApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<UserListDto> GetListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetUsersQuery(pageIndex, pageSize), cancellationToken);
    }

    public async Task<UserDto> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetUserByIdQuery(userId), cancellationToken);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto user, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new CreateUserCommand(user), cancellationToken);
    }

    public async Task<UserDto> UpdateAsync(Guid userId, UpdateUserDto user, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new UpdateUserCommand(userId, user), cancellationToken);
    }

    public async Task UpdatePasswordAsync(Guid userId, UpdateUserPasswordDto user, CancellationToken cancellationToken)
    {
        await _mediator.SendRequest(new UpdateUserPasswordCommand(userId, user.OldPassword, user.Password), cancellationToken);
    }

    public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken)
    {
        await _mediator.SendRequest(new DeleteUserCommand(userId), cancellationToken);
    }

    public async Task<UserRoleListDto> GetRoleListAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetUserRolesQuery(userId), cancellationToken);
    }

    public async Task AddRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        await _mediator.SendRequest(new AddUserToRoleCommand(userId, roleId), cancellationToken);
    }

    public async Task RemoveRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        await _mediator.SendRequest(new RemoveUserFromRoleCommand(userId, roleId), cancellationToken);
    }
}