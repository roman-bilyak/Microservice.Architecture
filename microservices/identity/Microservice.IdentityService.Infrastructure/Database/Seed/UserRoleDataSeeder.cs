using Microservice.Database;
using Microservice.IdentityService.Identity;

namespace Microservice.IdentityService.Database;

internal class UserRoleDataSeeder : IDataSeeder
{
    private readonly RoleManager _roleManager;
    private readonly UserManager _userManager;

    public UserRoleDataSeeder
    (
        RoleManager roleManager,
        UserManager userManager
    )
    {
        ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));
        ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        (await _roleManager.CreateAsync(new Role(Guid.NewGuid(), "admin"))).CheckErrors();

        (await _userManager.CreateAsync(new User(Guid.NewGuid(), "alice", "Alice", "Smith", "AliceSmith@gmail.com"), password: "alice")).CheckErrors();
        (await _userManager.CreateAsync(new User(Guid.NewGuid(), "bob", "Bob", "Smith", "BobSmith@gmail.com"), password: "bob")).CheckErrors();
    }
}
