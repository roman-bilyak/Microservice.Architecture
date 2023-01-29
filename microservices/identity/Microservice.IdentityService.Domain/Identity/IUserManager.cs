using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityService.Identity;

public interface IUserManager
{
    Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IdentityResult> CreateAsync(User user, string password, CancellationToken cancellationToken);

    Task<IdentityResult> AddPasswordAsync(User user, string password, CancellationToken cancellationToken);

    Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken);

    Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string password, CancellationToken cancellationToken);

    Task<IdentityResult> AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken);

    Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken);

    Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken);
}