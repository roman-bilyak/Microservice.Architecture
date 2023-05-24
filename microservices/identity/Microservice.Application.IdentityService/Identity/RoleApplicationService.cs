using MassTransit;
using MassTransit.Mediator;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.IdentityService.Identity;

[Authorize]
internal class RoleApplicationService : ApplicationService, IRoleApplicationService
{
    private readonly IMediator _mediator;

    public RoleApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<RoleListDto> GetListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetRolesQuery(pageIndex, pageSize), cancellationToken);
    }

    public async Task<RoleDto> GetAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new GetRoleByIdQuery(roleId), cancellationToken);
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto role, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new CreateRoleCommand(role), cancellationToken);
    }

    public async Task<RoleDto> UpdateAsync(Guid roleId, UpdateRoleDto role, CancellationToken cancellationToken)
    {
        return await _mediator.SendRequest(new UpdateRoleCommand(roleId, role), cancellationToken);
    }

    public async Task DeleteAsync(Guid roleId, CancellationToken cancellationToken)
    {
        await _mediator.SendRequest(new DeleteRoleCommand(roleId), cancellationToken);
    }
}