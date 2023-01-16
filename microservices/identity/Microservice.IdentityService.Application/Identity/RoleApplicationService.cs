using MassTransit.Mediator;
using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

internal class RoleApplicationService : ApplicationService, IRoleApplicationService
{
    private readonly IMediator _mediator;

    public RoleApplicationService(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _mediator = mediator;
    }

    public async Task<RoleDto> GetAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetRoleByIdQuery>();
        var response = await client.GetResponse<RoleDto>(new GetRoleByIdQuery(id), cancellationToken);
        return response.Message;
    }

    public async Task<RoleListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<GetRolesQuery>();
        var response = await client.GetResponse<RoleListDto>(new GetRolesQuery(pageIndex, pageSize), cancellationToken);
        return response.Message;
    }

    public async Task<RoleDto> CreateAsync([Required] CreateRoleDto role, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<CreateRoleCommand>();
        var response = await client.GetResponse<RoleDto>(new CreateRoleCommand(role), cancellationToken);
        return response.Message;
    }

    public async Task<RoleDto> UpdateAsync([Required] Guid id, [Required] UpdateRoleDto role, CancellationToken cancellationToken)
    {
        var client = _mediator.CreateRequestClient<UpdateRoleCommand>();
        var response = await client.GetResponse<RoleDto>(new UpdateRoleCommand(id, role), cancellationToken);
        return response.Message;
    }

    public async Task DeleteAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteRoleCommand(id), cancellationToken);
    }
}