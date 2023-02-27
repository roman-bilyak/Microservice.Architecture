using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.Tests;

public abstract class BaseIntegrationTests<TStartupModule>
    where TStartupModule : class, IStartupModule, new()
{
    private IApplication _application;
    private IServiceProvider _serviceProvider;

    [SetUp]
    public void Init()
    {
        IServiceCollection services = new ServiceCollection();

        ConfigurationBuilder builder = new();
        IConfiguration configuration = builder.Build();

        _application = services.AddApplication<TStartupModule>(configuration, x => ConfigureServices(x.Services));
        _application.ConfigureServices();

        _serviceProvider = services.BuildServiceProvider();

        _application.SetServiceProvider(_serviceProvider);
        _application.Configure();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {

    }

    protected T? GetService<T>()
    {
        return _serviceProvider.GetService<T>();
    }

    protected T GetRequiredService<T>()
        where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    [TearDown]
    public async Task Cleanup()
    {
        _application.Shutdown();
        _application.Dispose();

        if (_serviceProvider is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
        }
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}