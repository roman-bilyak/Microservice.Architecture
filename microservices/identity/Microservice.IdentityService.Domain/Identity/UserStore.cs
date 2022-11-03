using Microservice.Database;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityService.Identity;

public class UserStore :
    IUserStore<User>,
    IUserRoleStore<User>,
    IUserPasswordStore<User>
{
    private readonly IRepository<User> _userRepository;
    private readonly IRoleStore<Role> _roleStore;

    public UserStore
    (
        IRepository<User> userRepository,
        IRoleStore<Role> roleStore
    )
    {
        ArgumentNullException.ThrowIfNull(userRepository, nameof(userRepository));
        ArgumentNullException.ThrowIfNull(roleStore, nameof(roleStore));

        _userRepository = userRepository;
        _roleStore = roleStore;   
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult(user.Id.ToString());
    }

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult(user.Name);
    }

    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        user.Name = userName;
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult(user.NormalizedName);
    }

    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        user.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        await _userRepository.DeleteAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        FindUserByIdSpecification specification = new FindUserByIdSpecification(Guid.Parse(userId));
        return await _userRepository.SingleOrDefaultAsync(specification, cancellationToken);
    }

    public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(normalizedUserName, nameof(normalizedUserName));

        FindUserByNormalizedUserNameSpecification specification = new FindUserByNormalizedUserNameSpecification(normalizedUserName);
        return await _userRepository.SingleOrDefaultAsync(specification, cancellationToken);
    }

    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(passwordHash, nameof(passwordHash));

        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult(user.PasswordHash != null);
    }

    public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(roleName, nameof(roleName));

        Role role = await _roleStore.FindByNameAsync(roleName, cancellationToken);
        if (role == null)
        {
            throw new InvalidOperationException($"Role '{roleName}' doesn't exist");
        }

        user.AddRole(role.Id);
    }

    public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(roleName, nameof(roleName));

        Role role = await _roleStore.FindByNameAsync(roleName, cancellationToken);
        if (role == null)
        {
            return;
        }

        user.RemoveRole(role.Id);
    }

    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        List<string> roles = new List<string>();
        foreach (UserRole userRole in user.Roles)
        {
            Role role = await _roleStore.FindByIdAsync(userRole.RoleId.ToString(), cancellationToken);
            roles.Add(role.Name);
        }
        return roles;
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(roleName, nameof(roleName));

        Role role = await _roleStore.FindByNameAsync(roleName, cancellationToken);
        if (role == null)
        {
            return false;
        }

        return user.IsInRole(role.Id);
    }

    public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}