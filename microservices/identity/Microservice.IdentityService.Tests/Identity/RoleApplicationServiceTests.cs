using Microservice.Tests;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.IdentityService.Identity;

internal class RoleApplicationServiceTests : BaseIntegrationTests<IdentityServiceTestsModule>
{
    private IRoleApplicationService _roleApplicationService;

    [SetUp]
    public void Setup()
    {
        _roleApplicationService = ServiceProvider.GetRequiredService<IRoleApplicationService>();
    }

    [Test]
    public async Task GetList_Test()
    {
        await _roleApplicationService.GetListAsync(0, 10, CancellationToken.None);
    }
}