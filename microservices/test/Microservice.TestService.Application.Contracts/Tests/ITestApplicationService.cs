using Microservice.Application.Services;

namespace Microservice.TestService.Tests;

public interface ITestApplicationService : IApplicationService
{
    public Task GetAsync(CancellationToken cancellationToken);

    public Task SendMessageAsync(int id, string message, CancellationToken cancellationToken);
}