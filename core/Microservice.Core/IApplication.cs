using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public interface IApplication : IDisposable
{
    IServiceCollection Services { get; }

    IConfiguration Configuration { get; }

    void ConfigureServices();

    IServiceProvider GetServiceProvider();

    void SetServiceProvider(IServiceProvider serviceProvider);

    void Configure();

    void Shutdown();
}