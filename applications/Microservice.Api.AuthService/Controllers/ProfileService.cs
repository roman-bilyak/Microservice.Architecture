using IdentityServer4.Models;
using IdentityServer4.Services;
using Microservice.IdentityService.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Microservice.AuthService;

internal class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;

    public ProfileService(UserManager<User> userManager)
    {
        ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        User user = await _userManager.GetUserAsync(context.Subject);

        if (user == null)
        {
            return;
        }

        List<Claim> userClaims = new()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
        };

        context.IssuedClaims = userClaims;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }
}