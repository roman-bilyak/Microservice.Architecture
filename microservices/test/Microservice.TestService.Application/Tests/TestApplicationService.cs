using MassTransit;
using Microservice.Application;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.TestService.Tests;

[Authorize]
internal class TestApplicationService : ApplicationService, ITestApplicationService
{
    private readonly IBus _bus;

    public TestApplicationService(IBus bus)
    {
        _bus = bus;
    }

    public Task GetAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task SendMessageAsync(int testId, string message, CancellationToken cancellationToken)
    {
        TestMessage testMessage = new()
        {
            Id = testId,
            Message = message
        };

        await _bus.Publish(testMessage, cancellationToken);
    }
}