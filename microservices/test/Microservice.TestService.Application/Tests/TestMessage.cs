namespace Microservice.TestService.Tests;

public record TestMessage
{
    public int Id { get; set; }

    public string Message { get; set; }
}