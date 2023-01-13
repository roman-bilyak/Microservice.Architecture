using Microservice.Database;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityService.Identity;

public class RoleStore : IRoleStore<Role>
{
    private readonly IRepository<Role> _roleRepository;

    public RoleStore(IRepository<Role> roleRepository)
    {
        ArgumentNullException.ThrowIfNull(roleRepository, nameof(roleRepository));

        _roleRepository = roleRepository;
    }

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        await _roleRepository.AddAsync(role, cancellationToken);
        await _roleRepository.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        await _roleRepository.UpdateAsync(role, cancellationToken);
        await _roleRepository.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        await _roleRepository.DeleteAsync(role, cancellationToken);
        await _roleRepository.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        return Task.FromResult(role.Id.ToString());
    }

    public Task<string?> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(Role role, string? roleName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        ArgumentNullException.ThrowIfNull(roleName, nameof(roleName));

        role.Name = roleName;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(Role role, string? normalizedName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        ArgumentNullException.ThrowIfNull(normalizedName, nameof(normalizedName));

        role.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }

    public async Task<Role?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(roleId, nameof(roleId));

        return await _roleRepository.GetByIdAsync(Guid.Parse(roleId), cancellationToken);
    }

    public async Task<Role?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(normalizedRoleName, nameof(normalizedRoleName));

        FindRoleByNormalizedNameSpecification specification = new FindRoleByNormalizedNameSpecification(normalizedRoleName);
        return await _roleRepository.SingleOrDefaultAsync(specification, cancellationToken);
    }

    public void Dispose()
    {
    }
}