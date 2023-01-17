using MassTransit.Mediator;
using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

internal class RolesApplicationService : ApplicationService, IRolesApplicationService
{
    private readonly IMediator _mediator;

    public RolesApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<RoleListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetRolesQuery>();
        var response = await client.GetResponse<RoleListDto>(new GetRolesQuery(pageIndex, pageSize), cancellationToken);
        return response.Message;
    }

    public async Task<RoleDto> GetAsync([Required] Guid roleId, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetRoleByIdQuery>();
        var response = await client.GetResponse<RoleDto>(new GetRoleByIdQuery(roleId), cancellationToken);
        return response.Message;
    }

    public async Task<RoleDto> CreateAsync([Required] CreateRoleDto role, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateRoleCommand>();
        var response = await client.GetResponse<RoleDto>(new CreateRoleCommand(role), cancellationToken);
        return response.Message;
    }

    public async Task<RoleDto> UpdateAsync([Required] Guid roleId, [Required] UpdateRoleDto role, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<UpdateRoleCommand>();
        var response = await client.GetResponse<RoleDto>(new UpdateRoleCommand(roleId, role), cancellationToken);
        return response.Message;
    }

    public async Task DeleteAsync([Required] Guid roleId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteRoleCommand(roleId), cancellationToken);
    }
}