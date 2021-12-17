using Microservice.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microservice.AspNetCore.Conventions;

public class DynamicControllerConvention : IApplicationModelConvention
{
    private readonly DynamicControllerOptions _options;

    public DynamicControllerConvention(IOptions<DynamicControllerOptions> options)
    {
        _options = options.Value;
    }

    public void Apply(ApplicationModel application)
    {
        foreach (ControllerModel controller in application.Controllers)
        {
            Type controllerType = controller.ControllerType.AsType();

            controller.ControllerName = GetControllerName(controllerType);
            controller.ApiExplorer.GroupName = controller.ControllerName;
            controller.ApiExplorer.IsVisible = true;

            if (!controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
            {
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute("api")),
                });
            }

            foreach (ActionModel action in controller.Actions)
            {
                action.ActionName = action.ActionName.RemoveSuffix("Async");
                action.ApiExplorer.IsVisible = IsActionVisible(action.ActionName);

                action.Selectors.Where(x => x.AttributeRouteModel == null).ToList()
                    .ForEach(s => action.Selectors.Remove(s));

                SelectorModel selector = new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"{controller.ControllerName}/{action.ActionName}")),
                };

                string methodVerb = GetMethodVerbByActionName(action.ActionName);
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { methodVerb }));
                action.Selectors.Add(selector);

                foreach (ParameterModel parameter in action.Parameters)
                {
                    if (methodVerb != "GET" && methodVerb != "DELETE"
                        && !IsPrimitiveType(parameter.ParameterInfo.ParameterType)
                        && !typeof(CancellationToken).IsAssignableFrom(parameter.ParameterInfo.ParameterType))
                    {
                        parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                    }
                }
            }
        }
    }

    #region helper methods

    private string GetControllerName(Type controllerType)
    {
        string controllerName = controllerType.Name;
        if (controllerName.StartsWith("I") && controllerName.Length > 1 && char.IsUpper(controllerName[1]))
        {
            controllerName = controllerName.Substring(1, controllerName.Length - 1);
        }
        return controllerName.RemoveSuffix("Proxy").RemoveSuffix("ApplicationService");
    }

    private bool IsActionVisible(string actionName)
    {
        return actionName != "DynProxyGetTarget"
            && actionName != "DynProxySetTarget"
            && actionName != "GetInterceptors";
    }

    public static string GetMethodVerbByActionName(string actionName)
    {
        if (actionName.StartsWith("Get", StringComparison.OrdinalIgnoreCase))
        {
            return "GET";
        }

        if (actionName.StartsWith("Create", StringComparison.OrdinalIgnoreCase))
        {
            return "POST";
        }

        if (actionName.StartsWith("Update", StringComparison.OrdinalIgnoreCase))
        {
            return "PUT";
        }

        if (actionName.StartsWith("Delete", StringComparison.OrdinalIgnoreCase))
        {
            return "DELETE";
        }

        return "POST";
    }

    private static bool IsPrimitiveType(Type type)
    {
        return type.GetTypeInfo().IsPrimitive
            || type.GetTypeInfo().IsEnum
            || type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset)
            || type == typeof(TimeSpan)
            || type == typeof(Guid);
    }

    #endregion
}