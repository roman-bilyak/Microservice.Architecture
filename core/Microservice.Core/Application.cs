using Microservice.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

internal class Application : IApplication
{
    private readonly Action<ApplicationConfigurationOptions> _configurationOptionsAction;
    private readonly List<IModule> _modules;

    public Application(IServiceCollection services, IConfiguration configuration,
        Action<ApplicationConfigurationOptions> configurationOptionsAction)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        Configuration = configuration;
        Services = services;

        _configurationOptionsAction = configurationOptionsAction;
        _modules = new List<IModule>();
    }

    public IServiceCollection Services { get; private set; }

    public IConfiguration Configuration { get; private set; }

    public IServiceProvider ServiceProvider { get; private set; }

    public IApplication AddModule<T>() where T : class, IModule, new()
    {
        T module = Activator.CreateInstance<T>();
        module.Configuration = Configuration;

        _modules.Add(module);

        return this;
    }

    public virtual void ConfigureServices()
    {
        Services.AddSingleton<IApplication>(this);

        foreach (IModule module in _modules)
        {
            module.ConfigureServices(Services);
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

    public virtual void Configure()
    {
        foreach (IModule module in _modules)
        {
            module.Configure(ServiceProvider);
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