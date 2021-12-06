using Microservice.Core.Modularity;

namespace Microservice.Core;

public interface IApplication : IDisposable
{
    IApplication AddModule<T>() where T : IModule;

    void Configure();

    void SetServiceProvider(IServiceProvider serviceProvider);

    void Initialize();

    void Shutdown();
}