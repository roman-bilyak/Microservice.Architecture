using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Microservice.IdentityService.Identity;

//public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
//{
//    public UserClaimsPrincipalFactory
//    (
//        UserManager<User> userManager,
//        RoleManager<Role> roleManager,
//        IOptions<IdentityOptions> options
//    ) : base(userManager, roleManager, options)
//    {
//    }

//    public override async Task<ClaimsPrincipal> CreateAsync(User user)
//    {
//        ClaimsPrincipal principal = await base.CreateAsync(user);
//        ClaimsIdentity identity = principal.Identities.First();

//        if (!user.Name.IsNullOrWhiteSpace())
//        {
//            identity.AddIfNotContains(new Claim(AbpClaimTypes.Name, user.Name));
//        }

//        if (!user.Surname.IsNullOrWhiteSpace())
//        {
//            identity.AddIfNotContains(new Claim(AbpClaimTypes.SurName, user.Surname));
//        }

//        if (!user.PhoneNumber.IsNullOrWhiteSpace())
//        {
//            identity.AddIfNotContains(new Claim(AbpClaimTypes.PhoneNumber, user.PhoneNumber));
//        }

//        identity.AddIfNotContains(
//            new Claim(AbpClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed.ToString()));

//        if (!user.Email.IsNullOrWhiteSpace())
//        {
//            identity.AddIfNotContains(new Claim(AbpClaimTypes.Email, user.Email));
//        }

//        identity.AddIfNotContains(new Claim(AbpClaimTypes.EmailVerified, user.EmailConfirmed.ToString()));

//        return principal;
//    }
//}
