using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Microservice.IdentityService.Identity;

public class RoleManager : RoleManager<Role>, IRoleManager, IDomainService
{
    private CancellationToken _cancellationToken = CancellationToken.None;

    public RoleManager
    (
        IRoleStore<Role> store,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<Role>> logger
    ) : base(store, roleValidators, keyNormalizer, errors, logger)
    {
    }

    protected override CancellationToken CancellationToken => _cancellationToken;

    public async Task<Role?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await FindByIdAsync(id.ToString());
    }

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await CreateAsync(role);
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await UpdateAsync(role);
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return await DeleteAsync(role);
    }
}