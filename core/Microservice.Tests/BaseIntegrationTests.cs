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

    protected IServiceProvider ServiceProvider => _application.GetServiceProvider();

    [SetUp]
    public void Init()
    {
        IServiceCollection services = new ServiceCollection();

        ConfigurationBuilder builder = new();
        IConfiguration configuration = builder.Build();

        _application = services.AddApplication<TStartupModule>(configuration, x => ConfigureServices(x.Services));
        _application.ConfigureServices();
        _application.SetServiceProvider(services.BuildServiceProvider());
        _application.Configure();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {

    }

    [TearDown]
    public void Cleanup()
    {
        _application.Shutdown();
        _application.Dispose();
    }
}