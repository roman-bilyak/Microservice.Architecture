﻿using MassTransit;
using Microservice.Application;

namespace Microservice.TestService.Tests;

internal class TestsApplicationService : ApplicationService, ITestsApplicationService
{
    private readonly IBus _bus;

    public TestsApplicationService(IBus bus)
    {
        _bus = bus;
    }

    public Task GetAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task SendMessageAsync(int testId, string message, CancellationToken cancellationToken)
    {
        TestMessage testMessage = new TestMessage
        {
            Id = testId,
            Message = message
        };

        await _bus.Publish(testMessage, cancellationToken);
    }
}