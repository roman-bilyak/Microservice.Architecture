using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microservice.IdentityService.Identity;

public class UserManager : UserManager<User>, IUserManager, IDomainService
{
    private CancellationToken _cancellationToken = CancellationToken.None;

    public UserManager
    (
        IUserStore<User> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<User>> logger
    ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    protected override CancellationToken CancellationToken => _cancellationToken;

    public async Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await FindByIdAsync(id.ToString());
    }

    public async Task<IdentityResult> CreateAsync(User user, string password, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await CreateAsync(user, password);
    }

    public async Task<IdentityResult> AddPasswordAsync(User user, string password, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await AddPasswordAsync(user, password);
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await UpdateAsync(user);
    }

    public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string password, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await ChangePasswordAsync(user, oldPassword, password);
    }

    public async Task<IdentityResult> AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await RemoveFromRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await DeleteAsync(user);
    }
}