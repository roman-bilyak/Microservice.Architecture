using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public interface IRolesApplicationService : IApplicationService
{
    public Task<RoleListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<RoleDto> GetAsync([Required] Guid roleId, CancellationToken cancellationToken);

    public Task<RoleDto> CreateAsync([Required] CreateRoleDto role, CancellationToken cancellationToken);

    public Task<RoleDto> UpdateAsync([Required] Guid roleId, [Required] UpdateRoleDto role, CancellationToken cancellationToken);

    public Task DeleteAsync([Required] Guid roleId, CancellationToken cancellationToken);
}