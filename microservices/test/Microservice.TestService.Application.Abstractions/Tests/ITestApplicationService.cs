using Microservice.Application;

namespace Microservice.TestService.Tests;

/// <summary>
/// Provides methods for testing the application.
/// </summary>
public interface ITestApplicationService : IApplicationService
{
    /// <summary>
    /// Test method.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    public Task GetAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Sends a message for a given test id and message.
    /// </summary>
    /// <param name="testId">The id of the test.</param>
    /// <param name="message">The message to send.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    public Task SendMessageAsync(int testId, string message, CancellationToken cancellationToken);
}