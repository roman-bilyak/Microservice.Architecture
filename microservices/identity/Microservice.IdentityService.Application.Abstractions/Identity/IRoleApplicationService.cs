using Microservice.Application;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public interface IRoleApplicationService : IApplicationService
{
    public Task<RoleDto> GetAsync([Required] Guid id, CancellationToken cancellationToken);

    public Task<RoleListDto> GetListAsync([Required] int pageIndex, [Required] int pageSize, CancellationToken cancellationToken);

    public Task<RoleDto> CreateAsync([Required] CreateRoleDto role, CancellationToken cancellationToken);

    public Task<RoleDto> UpdateAsync([Required] Guid id, [Required] UpdateRoleDto role, CancellationToken cancellationToken);

    public Task DeleteAsync([Required] Guid id, CancellationToken cancellationToken);
}