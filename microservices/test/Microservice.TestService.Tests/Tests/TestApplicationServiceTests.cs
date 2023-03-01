using NUnit.Framework;

namespace Microservice.TestService.Tests;

[TestFixture]
internal class TestApplicationServiceTests : TestServiceTests
{
    private ITestApplicationService _testApplicationService;

    [SetUp]
    public void Setup()
    {
        _testApplicationService = GetRequiredService<ITestApplicationService>();
    }
}