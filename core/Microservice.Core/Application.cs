using Microservice.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.Core;

internal class Application<TStartupModule> : IApplication
    where TStartupModule : class, IStartupModule, new()
{
    private readonly Action<ApplicationConfigurationOptions>? _configurationOptionsAction;
    private List<IStartupModule>? _modules;
    private IServiceProvider? _serviceProvider;

    public Application
    (
        IServiceCollection services,
        IConfiguration configuration,
        Action<ApplicationConfigurationOptions>? configurationOptionsAction
    )
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

    public virtual void ConfigureServices()
    {
        Services.AddSingleton<IApplication>(this);

        Modules.ForEach(x => x.PreConfigureServices(Services, Configuration));
        Modules.ForEach(x => x.ConfigureServices(Services, Configuration));
        Modules.ForEach(x => x.PostConfigureServices(Services, Configuration));

        ApplicationConfigurationOptions configurationOptions = new(Services);
        _configurationOptionsAction?.Invoke(configurationOptions);
    }

    public virtual IServiceProvider GetServiceProvider()
    {
        EnsureInitialized();

        return _serviceProvider;
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

    public virtual void Configure()
    {
        EnsureInitialized();

        Modules.ForEach(x => x.PreConfigure(_serviceProvider));
        Modules.ForEach(x => x.Configure(_serviceProvider));
        Modules.ForEach(x => x.PostConfigure(_serviceProvider));
    }

    public virtual void Shutdown()
    {
        EnsureInitialized();

        Modules.ForEach(x => x.Shutdown(_serviceProvider));
    }

    public virtual void Dispose()
    {
    }

    #region helper methods

    [MemberNotNull(nameof(_serviceProvider))]
    private void EnsureInitialized()
    {
        if (_serviceProvider is null)
        {
            throw new InvalidOperationException($"{nameof(_serviceProvider)} not yet been initialized.");
        }
    }

    #endregion
}