using MassTransit.Mediator;
using Microservice.Application;

namespace Microservice.IdentityService.Identity;

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
        var client = _mediator.CreateRequestClient<GetUsersQuery>();
        var response = await client.GetResponse<UserListDto>(new GetUsersQuery(pageIndex, pageSize), cancellationToken);
        return response.Message;
    }

    public async Task<UserDto> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserByIdQuery>();
        var response = await client.GetResponse<UserDto>(new GetUserByIdQuery(userId), cancellationToken);
        return response.Message;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto user, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateUserCommand>();
        var response = await client.GetResponse<UserDto>(new CreateUserCommand(user), cancellationToken);
        return response.Message;
    }

    public async Task<UserDto> UpdateAsync(Guid userId, UpdateUserDto user, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<UpdateUserCommand>();
        var response = await client.GetResponse<UserDto>(new UpdateUserCommand(userId, user), cancellationToken);
        return response.Message;
    }

    public async Task UpdatePasswordAsync(Guid userId, UpdateUserPasswordDto user, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateUserPasswordCommand(userId, user.OldPassword, user.Password), cancellationToken);
    }

    public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand(userId), cancellationToken);
    }

    public async Task<UserRoleListDto> GetRoleListAsync(Guid userId, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserRolesQuery>();
        var response = await client.GetResponse<UserRoleListDto>(new GetUserRolesQuery(userId), cancellationToken);
        return response.Message;
    }

    public async Task AddRoleAsync(Guid userId, string roleName, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AddUserToRoleCommand(userId, roleName), cancellationToken);
    }

    public async Task RemoveRoleAsync(Guid userId, string roleName, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveUserFromRoleCommand(userId, roleName), cancellationToken);
    }
}