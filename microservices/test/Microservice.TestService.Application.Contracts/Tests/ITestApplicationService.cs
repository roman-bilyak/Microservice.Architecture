using Microservice.Core.Services;

namespace Microservice.TestService.Tests;

public interface ITestApplicationService : IApplicationService
{
    public Task GetEmpty(CancellationToken cancellationToken);
}