using Microservice.Application;

namespace Microservice.TestService.Tests;

public interface ITestsApplicationService : IApplicationService
{
    public Task GetAsync(CancellationToken cancellationToken);

    public Task SendMessageAsync(int testId, string message, CancellationToken cancellationToken);
}