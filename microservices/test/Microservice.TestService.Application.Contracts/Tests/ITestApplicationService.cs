using Microservice.Core.Application.Services;

namespace Microservice.TestService.Tests;

public interface ITestApplicationService : IApplicationService
{
    public Task GetAsync(CancellationToken cancellationToken);
}