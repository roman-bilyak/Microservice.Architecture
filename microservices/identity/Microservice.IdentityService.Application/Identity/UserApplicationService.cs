﻿using MassTransit.Mediator;
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

    public async Task<UserDto> GetUserAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserByIdQuery>();
        var response = await client.GetResponse<UserDto>(new GetUserByIdQuery(id), cancellationToken);
        return response.Message;
    }

    public async Task<UserListDto> GetUsersAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUsersQuery>();
        var response = await client.GetResponse<UserListDto>(new GetUsersQuery(pageIndex, pageSize), cancellationToken);
        return response.Message;
    }

    public async Task<UserDto> CreateUserAsync([Required] CreateUserDto user, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateUserCommand>();
        var response = await client.GetResponse<UserDto>(new CreateUserCommand(user), cancellationToken);
        return response.Message;
    }

    public async Task<UserDto> UpdateUserAsync([Required] Guid id, [Required] UpdateUserDto user, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<UpdateUserCommand>();
        var response = await client.GetResponse<UserDto>(new UpdateUserCommand(id, user), cancellationToken);
        return response.Message;
    }

    public async Task ChangeUserPasswordAsync([Required] Guid id, [Required] ChangeUserPasswordDto user, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ChangeUserPasswordCommand(id, user.OldPassword, user.Password), cancellationToken);
    }

    public async Task<UserRoleListDto> GetUserRolesAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetUserRolesQuery>();
        var response = await client.GetResponse<UserRoleListDto>(new GetUserRolesQuery(id), cancellationToken);
        return response.Message;
    }

    public async Task AddUserToRoleAsync([Required] Guid id, [Required] string roleName, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AddUserToRoleCommand(id, roleName), cancellationToken);
    }

    public async Task RemoveUserFromRoleAsync([Required] Guid id, [Required] string roleName, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveUserFromRoleCommand(id, roleName), cancellationToken);
    }

    public async Task DeleteUserAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
    }
}