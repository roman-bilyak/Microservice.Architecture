using Microservice.Core.Services;

namespace Microservice.TestService.Tests;

internal class TestApplicationService : ApplicationService, ITestApplicationService
{
    public Task GetEmpty(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}