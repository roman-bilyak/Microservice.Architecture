using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public interface IApplication : IDisposable
{
    IServiceCollection Services { get; }

    IConfiguration Configuration { get; }

    IServiceProvider? ServiceProvider { get; }

    void ConfigureServices();

    void SetServiceProvider(IServiceProvider serviceProvider);

    void Configure();

    void Shutdown();
}