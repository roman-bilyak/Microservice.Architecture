using MassTransit;
using Microsoft.Extensions.Logging;

namespace Microservice.TestService.Tests;

public class TestMessageConsumer : IConsumer<TestMessage>
{
    readonly ILogger<TestMessageConsumer> _logger;

    public TestMessageConsumer(ILogger<TestMessageConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TestMessage> context)
    {
        _logger.LogInformation("Test Message: {Id} - {Message}", context.Message?.Id, context.Message?.Message);
        return Task.CompletedTask;
    }
}