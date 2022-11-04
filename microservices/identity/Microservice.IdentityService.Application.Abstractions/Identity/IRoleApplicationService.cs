using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public interface IRoleApplicationService : IApplicationService
{
    public Task<RoleDto> GetRoleAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task<RoleListDto> GetRolesAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<RoleDto> CreateRoleAsync([Required] CreateRoleDto role, CancellationToken cancellationToken);

    public Task<RoleDto> UpdateRoleAsync([Required] Guid id, [Required] UpdateRoleDto role, CancellationToken cancellationToken);

    public Task DeleteRoleAsync([Required] Guid id, CancellationToken cancellationToken);
}