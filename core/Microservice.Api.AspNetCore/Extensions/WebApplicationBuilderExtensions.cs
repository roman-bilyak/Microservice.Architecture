using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Builder;

namespace Microservice.AspNetCore;

public static class WebApplicationBuilderExtensions
{
    public static IApplication AddApplication<TStartupModule>(this WebApplicationBuilder builder,
        Action<ApplicationConfigurationOptions>? configurationOptionsAction = null)
        where TStartupModule : class, IStartupModule, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.Services.AddApplication<TStartupModule>(builder.Configuration, configurationOptionsAction);
    }
}