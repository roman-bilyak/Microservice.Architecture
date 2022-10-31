﻿using MassTransit;
using Microservice.Application;

namespace Microservice.TestService.Tests;

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

    public async Task SendMessageAsync(int id, string message, CancellationToken cancellationToken)
    {
        TestMessage testMessage = new TestMessage
        {
            Id = id,
            Message = message
        };

        await _bus.Publish(testMessage, cancellationToken);
    }
}