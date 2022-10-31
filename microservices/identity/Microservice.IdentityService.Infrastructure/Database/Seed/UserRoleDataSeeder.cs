using Microservice.Database;
using Microservice.IdentityService.Identity;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityService.Database;

internal class UserRoleDataSeeder : IDataSeeder
{
    private readonly UserManager _userManager;

    public UserRoleDataSeeder(UserManager userManager)
    {
        ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

        _userManager = userManager;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        (await _userManager.CreateAsync(new User(Guid.NewGuid(), "alice", "Alice", "Smith", "AliceSmith@gmail.com"), password: "alice")).CheckErrors();
        (await _userManager.CreateAsync(new User(Guid.NewGuid(), "bob", "Bob", "Smith", "BobSmith@gmail.com"), password: "bob")).CheckErrors();
    }
}
