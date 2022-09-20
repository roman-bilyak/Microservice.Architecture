using Microservice.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public interface IApplication : IDisposable
{
    IServiceCollection Services { get; }

    IServiceProvider ServiceProvider { get; }

    IApplication AddModule<T>() where T : class, IStartupModule, new();

    void ConfigureServices();

    void SetServiceProvider(IServiceProvider serviceProvider);

    void Configure();

    void Shutdown();
}