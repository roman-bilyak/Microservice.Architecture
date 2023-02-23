using Microservice.Tests;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.IdentityService.Identity;

internal class UserApplicationServiceTests : BaseIntegrationTests<IdentityServiceTestsModule>
{
    private IUserApplicationService _userApplicationService;

    [SetUp]
    public void Setup()
    {
        _userApplicationService = ServiceProvider.GetRequiredService<IUserApplicationService>();
    }

    [Test]
    public async Task GetList_Test()
    {
        await _userApplicationService.GetListAsync(0, 10, CancellationToken.None);
    }
}