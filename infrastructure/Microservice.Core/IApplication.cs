using Microservice.Core.Modularity;

namespace Microservice.Core;

public interface IApplication : IDisposable
{
    IApplication AddModule<T>() where T : class, IModule, new();

    void Configure();

    void SetServiceProvider(IServiceProvider serviceProvider);

    void Initialize();

    void Shutdown();
}