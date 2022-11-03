using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityService.Identity;

public interface IRoleManager
{
    Task<Role> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken);

    Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken);

    Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken);
}