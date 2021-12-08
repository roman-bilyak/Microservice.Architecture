using Microservice.Core.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Microservice.AspNetCore.Providers;

public class ApplicationServiceControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        Type type = typeInfo.AsType();
        return typeof(IApplicationService).IsAssignableFrom(type)
            && !typeInfo.IsAbstract && !typeInfo.IsGenericType;
    }
}