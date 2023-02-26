using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.ReviewService.Reviews;

[TestFixture]
internal class UserApplicationServiceTests : ReviewServiceTests
{
    private IUserApplicationService _userApplicationService;

    [SetUp]
    public void Setup()
    {
        _userApplicationService = ServiceProvider.GetRequiredService<IUserApplicationService>();
    }
}