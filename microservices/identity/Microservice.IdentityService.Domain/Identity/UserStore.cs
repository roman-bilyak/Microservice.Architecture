using Microservice.Database;
using Microsoft.AspNetCore.Identity;
using System;

namespace Microservice.IdentityService.Identity;

public class UserStore :
    IUserStore<User>,
    IUserEmailStore<User>,
    IUserPasswordStore<User>,
    IUserRoleStore<User>
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

    public Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult<string?>(user.Name);
    }

    public Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(userName, nameof(userName));

        user.SetName(userName);
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult<string?>(user.NormalizedName);
    }

    public Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(normalizedName, nameof(normalizedName));

        user.SetNormalizedName(normalizedName);
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

    public async Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        FindUserByIdSpecification specification = new(Guid.Parse(userId));
        return await _userRepository.SingleOrDefaultAsync(specification, cancellationToken);
    }

    public async Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(normalizedUserName, nameof(normalizedUserName));

        FindUserByNormalizedNameSpecification specification = new(normalizedUserName);
        return await _userRepository.SingleOrDefaultAsync(specification, cancellationToken);
    }

    public Task<string?> GetEmailAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult<string?>(user.Email);
    }

    public Task<string?> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult<string?>(user.NormalizedEmail);
    }

    public Task SetEmailAsync(User user, string? email, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(email, nameof(email));

        user.SetEmail(email);
        return Task.CompletedTask;
    }

    public Task SetNormalizedEmailAsync(User user, string? normalizedEmail, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(normalizedEmail, nameof(normalizedEmail));

        user.SetNormalizedEmail(normalizedEmail);
        return Task.CompletedTask;
    }

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult(user.IsEmailConfirmed);
    }

    public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        user.SetEmailConfirmed(confirmed);
        return Task.CompletedTask;
    }

    public async Task<User?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(normalizedEmail, nameof(normalizedEmail));

        FindUserByNormalizedEmailSpecification specification = new(normalizedEmail);
        return await _userRepository.SingleOrDefaultAsync(specification, cancellationToken);
    }

    public Task SetPasswordHashAsync(User user, string? passwordHash, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(passwordHash, nameof(passwordHash));

        user.SetPasswordHash(passwordHash);
        return Task.CompletedTask;
    }

    public Task<string?> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return Task.FromResult<string?>(user.PasswordHash);
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

        Role? role = await _roleStore.FindByNameAsync(roleName, cancellationToken);
        if (role is null)
        {
            throw new InvalidOperationException($"Role '{roleName}' doesn't exist");
        }

        user.AddRole(role.Id);
    }

    public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(roleName, nameof(roleName));

        Role? role = await _roleStore.FindByNameAsync(roleName, cancellationToken);
        if (role is null)
        {
            return;
        }

        user.RemoveRole(role.Id);
    }

    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        List<string> roles = new();
        foreach (UserRole userRole in user.Roles)
        {
            Role? role = await _roleStore.FindByIdAsync(userRole.RoleId.ToString(), cancellationToken);
            if (role is not null)
            {
                roles.Add(role.Name);
            }
        }
        return roles;
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(roleName, nameof(roleName));

        Role? role = await _roleStore.FindByNameAsync(roleName, cancellationToken);
        if (role is null)
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