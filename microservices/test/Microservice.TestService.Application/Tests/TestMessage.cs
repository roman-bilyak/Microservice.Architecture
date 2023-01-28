namespace Microservice.TestService.Tests;

public record TestMessage
{
    public int Id { get; init; }

    public string Message { get; init; } = string.Empty;
}