using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.Tests;

public class BaseIntegrationTests<TStartupModule>
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

        _application = services.AddApplication<TStartupModule>(configuration);
        _application.ConfigureServices();
        _application.SetServiceProvider(services.BuildServiceProvider());
        _application.Configure();
    }

    [TearDown]
    public void Cleanup()
    {
        _application.Shutdown();
        _application.Dispose();
    }
}