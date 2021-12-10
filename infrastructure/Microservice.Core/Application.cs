using Microservice.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public class Application : IApplication
{
    private readonly IServiceCollection _services;
    private readonly Action<ApplicationConfigurationOptions> _configurationOptionsAction;
    private readonly List<IModule> _modules;
    private IServiceProvider _serviceProvider;

    public Application(IServiceCollection services, Action<ApplicationConfigurationOptions> configurationOptionsAction)
    {
        ArgumentNullException.ThrowIfNull(services);

        _services = services;
        _configurationOptionsAction = configurationOptionsAction;
        _modules = new List<IModule>();
    }

    public IApplication AddModule<T>() where T : class, IModule, new()
    {
        _modules.Add(Activator.CreateInstance<T>());

        return this;
    }

    public virtual void Configure()
    {
        _services.AddSingleton<IApplication>(this);

        foreach(IModule module in _modules)
        {
            module.Configure(_services);
        }

        ApplicationConfigurationOptions configurationOptions = new ApplicationConfigurationOptions(_services);
        _configurationOptionsAction?.Invoke(configurationOptions);
    }

    public virtual void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        if (_serviceProvider != null && _serviceProvider != serviceProvider)
        {
            throw new Exception("Service provider was already set before to another service provider instance.");
        }

        _serviceProvider = serviceProvider;
    }

    public virtual void Initialize()
    {
        foreach (IModule module in _modules)
        {
            module.Initialize(_serviceProvider);
        }
    }

    public virtual void Shutdown()
    {
        foreach (IModule module in _modules)
        {
            module.Shutdown(_serviceProvider);
        }
    }

    public virtual void Dispose()
    {
    }
}