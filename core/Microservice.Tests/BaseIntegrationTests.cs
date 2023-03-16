using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

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

    protected Mock<ICurrentPrincipleAccessor> CurrentPrincipleAccessor = new();

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        CurrentPrincipleAccessor.Setup(x => x.Principal)
            .Returns
            (
                new ClaimsPrincipal
                (
                    new ClaimsIdentity
                    (
                        new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, "afa85f64-5717-4562-b3fc-2c963f66afa6"),
                            new Claim(ClaimTypes.Name, "admin"),
                            new Claim(ClaimTypes.GivenName, "System"),
                            new Claim(ClaimTypes.Surname, "Administrator"),
                            new Claim(ClaimTypes.Email, "admin@test.com"),
                        }
                    )
                )
            );

        services.AddTransient(x => CurrentPrincipleAccessor.Object);
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