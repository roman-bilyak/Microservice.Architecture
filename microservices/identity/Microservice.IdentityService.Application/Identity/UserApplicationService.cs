using MassTransit.Mediator;
using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

internal class UserApplicationService : ApplicationService, IUserApplicationService
{
    private readonly IMediator _mediator;

    public UserApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<UserListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUsersQuery>();
        var response = await client.GetResponse<UserListDto>(new GetUsersQuery(pageIndex, pageSize), cancellationToken);
        return response.Message;
    }

    public async Task<UserDto> GetAsync([Required] Guid userId, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserByIdQuery>();
        var response = await client.GetResponse<UserDto>(new GetUserByIdQuery(userId), cancellationToken);
        return response.Message;
    }

    public async Task<UserDto> CreateAsync([Required] CreateUserDto user, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateUserCommand>();
        var response = await client.GetResponse<UserDto>(new CreateUserCommand(user), cancellationToken);
        return response.Message;
    }

    public async Task<UserDto> UpdateAsync([Required] Guid userId, [Required] UpdateUserDto user, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<UpdateUserCommand>();
        var response = await client.GetResponse<UserDto>(new UpdateUserCommand(userId, user), cancellationToken);
        return response.Message;
    }

    public async Task UpdatePasswordAsync([Required] Guid userId, [Required] UpdateUserPasswordDto user, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateUserPasswordCommand(userId, user.OldPassword, user.Password), cancellationToken);
    }

    public async Task DeleteAsync([Required] Guid userId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand(userId), cancellationToken);
    }

    public async Task<UserRoleListDto> GetRoleListAsync([Required] Guid userId, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserRolesQuery>();
        var response = await client.GetResponse<UserRoleListDto>(new GetUserRolesQuery(userId), cancellationToken);
        return response.Message;
    }

    public async Task AddRoleAsync([Required] Guid userId, [Required] string roleName, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AddUserToRoleCommand(userId, roleName), cancellationToken);
    }

    public async Task RemoveRoleAsync([Required] Guid userId, [Required] string roleName, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveUserFromRoleCommand(userId, roleName), cancellationToken);
    }
}