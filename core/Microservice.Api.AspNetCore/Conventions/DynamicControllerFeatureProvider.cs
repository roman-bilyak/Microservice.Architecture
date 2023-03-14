using Microservice.Core;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Microservice.AspNetCore;

public class DynamicControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private readonly IApplication _application;

    public DynamicControllerFeatureProvider(IApplication application)
    {
        _application = application;
    }

    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        DynamicControllerOptions dynamicControllerOptions = _application.GetServiceProvider().GetOptions<DynamicControllerOptions>();

        foreach (IApplicationPartTypeProvider part in parts.OfType<IApplicationPartTypeProvider>())
        {
            foreach (TypeInfo type in part.Types)
            {
                if (dynamicControllerOptions.IsController(type))
                {
                    TypeInfo? implementationType = _application.Services.GetImplementationType(type.AsType())?.GetTypeInfo();
                    TypeInfo controllerType = implementationType ?? type;
                    if (controllerType.IsClass
                        && !controllerType.IsAbstract
                        && !controllerType.IsGenericType
                        && !feature.Controllers.Contains(controllerType))
                    {
                        feature.Controllers.Add(controllerType);
                    }
                }
            }
        }
    }
}