using Microservice.IdentityService.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.IdentityService.Migrations
{
    public partial class AddUsersAndRoles : Migration
    {
        private readonly List<Role> _roles = new()
        {
            new Role(Guid.NewGuid(), "admin")
        };

        private readonly List<(User User, string Password)> _usersData = new()
        {
            (new User(Guid.NewGuid(), "alice", "Alice", "Smith", "AliceSmith@gmail.com"), "alice"),
            (new User(Guid.NewGuid(), "bob", "Bob", "Smith", "BobSmith@gmail.com"), "bob")
        };

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ILookupNormalizer normalizer = new UpperInvariantLookupNormalizer();
            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            foreach (var role in _roles)
            {
                role.SetNormalizedName(normalizer.NormalizeName(role.Name));

                migrationBuilder.Sql($"INSERT INTO [dbo].[Roles] ([Id], [Name], [NormalizedName]) " +
                    $"VALUES(CAST('{role.Id}' AS UNIQUEIDENTIFIER), '{role.Name}', '{role.NormalizedName}')");
            }

            foreach (var userData in _usersData)
            {
                User user = userData.User;
                user.SetNormalizedName(normalizer.NormalizeName(user.Name));
                user.SetNormalizedEmail(normalizer.NormalizeEmail(user.Email));
                user.SetPasswordHash(passwordHasher.HashPassword(user, userData.Password));

                migrationBuilder.Sql($"INSERT INTO [dbo].[Users] ([Id], [Name], [NormalizedName], [FirstName], [LastName], [Email], [NormalizedEmail], [IsEmailConfirmed], [PasswordHash]) " +
                    $"VALUES(CAST('{user.Id}' AS UNIQUEIDENTIFIER), '{user.Name}', '{user.NormalizedName}', '{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.NormalizedEmail}', 0, '{user.PasswordHash}')");

                foreach (var role in _roles)
                {
                    migrationBuilder.Sql($"INSERT INTO [dbo].[UserRoles] ([UserId], [RoleId]) VALUES(CAST('{user.Id}' AS UNIQUEIDENTIFIER), CAST('{role.Id}' AS UNIQUEIDENTIFIER))");
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}