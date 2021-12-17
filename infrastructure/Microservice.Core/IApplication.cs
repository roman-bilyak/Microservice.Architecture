using Microservice.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public interface IApplication : IDisposable
{
    IServiceCollection Services { get; }

    IServiceProvider ServiceProvider { get; }

    IApplication AddModule<T>() where T : class, IModule, new();

    void Configure();

    void SetServiceProvider(IServiceProvider serviceProvider);

    void Initialize();

    void Shutdown();
}