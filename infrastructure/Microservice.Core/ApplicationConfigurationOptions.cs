using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public class ApplicationConfigurationOptions
{
    public IServiceCollection Services { get; }

    public ApplicationConfigurationOptions(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }
}