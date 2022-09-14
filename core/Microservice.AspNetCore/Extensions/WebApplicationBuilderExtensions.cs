﻿using Microservice.Core;
using Microsoft.AspNetCore.Builder;

namespace Microservice.AspNetCore;

public static class WebApplicationBuilderExtensions
{
    public static IApplication AddApplication(this WebApplicationBuilder builder,
        Action<ApplicationConfigurationOptions> configurationOptionsAction = null)
    {
        return builder.Services.AddApplication(builder.Configuration, configurationOptionsAction);
    }
}