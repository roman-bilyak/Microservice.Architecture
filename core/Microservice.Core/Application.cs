using Microservice.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Core;

internal class Application<TStartupModule> : IApplication
    where TStartupModule : class, IStartupModule, new()
{
    private readonly Action<ApplicationConfigurationOptions>? _configurationOptionsAction;
    private List<IStartupModule>? _modules;

    public Application(IServiceCollection services, IConfiguration configuration,
        Action<ApplicationConfigurationOptions>? configurationOptionsAction)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        Services = services;
        Configuration = configuration;

        _configurationOptionsAction = configurationOptionsAction;
    }

    protected List<IStartupModule> Modules
    {
        get
        {
            return _modules ??= StartupModuleHelper.GetModules<TStartupModule>().ToList();
        }
    }

    public IServiceCollection Services { get; private set; }

    public IConfiguration Configuration { get; private set; }

    public IServiceProvider? ServiceProvider { get; private set; }

    public virtual void ConfigureServices()
    {
        Services.AddSingleton<IApplication>(this);
        Services.Replace(ServiceDescriptor.Singleton(Configuration));

        Modules.ForEach(x => x.PreConfigureServices(Services));
        Modules.ForEach(x => x.ConfigureServices(Services));
        Modules.ForEach(x => x.PostConfigureServices(Services));

        ApplicationConfigurationOptions configurationOptions = new(Services);
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
        EnsureInitialized();

        Modules.ForEach(x => x.PreConfigure(ServiceProvider));
        Modules.ForEach(x => x.Configure(ServiceProvider));
        Modules.ForEach(x => x.PostConfigure(ServiceProvider));
    }

    public virtual void Shutdown()
    {
        EnsureInitialized();

        Modules.ForEach(x => x.Shutdown(ServiceProvider));
    }

    public virtual void Dispose()
    {
    }

    #region helper methods

    [MemberNotNull(nameof(ServiceProvider))]
    private void EnsureInitialized()
    {
        if (ServiceProvider is null)
        {
            throw new InvalidOperationException($"{nameof(ServiceProvider)} not yet been initialized.");
        }
    }

    #endregion
}