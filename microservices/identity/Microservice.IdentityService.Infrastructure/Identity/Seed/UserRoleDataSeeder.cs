using Microservice.Database;

namespace Microservice.IdentityService.Identity;

internal class UserRoleDataSeeder : IDataSeeder
{
    private readonly RoleManager _roleManager;
    private readonly UserManager _userManager;
    private readonly List<Role> _roles = new List<Role>
    {
        new Role(Guid.NewGuid(), "admin")
    };

    private readonly List<(User User, string Password, string[] Roles)> _users = new List<(User, string, string[])>
    {
        (new User(Guid.NewGuid(), "alice", "Alice", "Smith", "AliceSmith@gmail.com"), "alice", new []{ "admin" }),
        (new User(Guid.NewGuid(), "bob", "Bob", "Smith", "BobSmith@gmail.com"), "bob", new []{ "admin" })
    };

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
        foreach (var role in _roles)
        {
            await AddRoleIfNotExistsAsync(role);
        }

        foreach (var user in _users)
        {
            await AddUserIfNotExistsAsync(user.User, user.Password, user.Roles);
        }
    }

    #region helper methods

    private async Task AddRoleIfNotExistsAsync(Role role)
    {
        if (!await _roleManager.RoleExistsAsync(role.Name))
        {
            (await _roleManager.CreateAsync(role)).CheckErrors();
        }
    }

    private async Task AddUserIfNotExistsAsync(User user, string password, params string[] roles)
    {
        if (await _userManager.FindByNameAsync(user.Name) == null)
        {
            (await _userManager.CreateAsync(user, password)).CheckErrors();
            (await _userManager.AddToRolesAsync(user, roles)).CheckErrors();
        }
    }

    #endregion
}
