using Microservice.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

internal class Application : IApplication
{
    private readonly Action<ApplicationConfigurationOptions> _configurationOptionsAction;
    private readonly List<IModule> _modules;

    public Application(IServiceCollection services, Action<ApplicationConfigurationOptions> configurationOptionsAction)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
        _configurationOptionsAction = configurationOptionsAction;
        _modules = new List<IModule>();
    }

    public IServiceCollection Services { get; private set; }

    public IServiceProvider ServiceProvider { get; private set; }

    public IApplication AddModule<T>() where T : class, IModule, new()
    {
        _modules.Add(Activator.CreateInstance<T>());

        return this;
    }

    public virtual void Configure()
    {
        Services.AddSingleton<IApplication>(this);

        foreach (IModule module in _modules)
        {
            module.Configure(Services);
        }

        ApplicationConfigurationOptions configurationOptions = new ApplicationConfigurationOptions(Services);
        _configurationOptionsAction?.Invoke(configurationOptions);
    }

    public virtual void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        if (ServiceProvider != null && ServiceProvider != serviceProvider)
        {
            throw new Exception("Service provider was already set before to another service provider instance.");
        }

        ServiceProvider = serviceProvider;
    }

    public virtual void Initialize()
    {
        foreach (IModule module in _modules)
        {
            module.Initialize(ServiceProvider);
        }
    }

    public virtual void Shutdown()
    {
        foreach (IModule module in _modules)
        {
            module.Shutdown(ServiceProvider);
        }
    }

    public virtual void Dispose()
    {
    }
}