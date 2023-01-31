using Microservice.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microservice.AspNetCore;

public class DynamicControllerConvention : IApplicationModelConvention
{
    private readonly DynamicControllerOptions _options;
    private static readonly string[] _prefixes = new string[] { "Get", "Add", "Create", "Update", "Delete", "Remove" };

    public DynamicControllerConvention(IOptions<DynamicControllerOptions> options)
    {
        _options = options.Value;
    }

    public void Apply(ApplicationModel application)
    {
        foreach (ControllerModel controller in application.Controllers)
        {
            Type controllerType = controller.ControllerType.AsType();
            if (!_options.IsController(controllerType))
            {
                continue;
            }

            NormalizeName(controller);
            SetGroupName(controller);

            SetVisibility(controller);
            if (!IsVisible(controller))
            {
                continue;
            }

            ConfigureRoute(controller);

            foreach (ActionModel action in controller.Actions)
            {
                ConfigureAction(action);
            }
        }
    }

    #region helper methods

    private static void NormalizeName(ControllerModel controller)
    {
        string controllerName = controller.ControllerType.AsType().Name;
        if (controllerName.StartsWith("I") && controllerName.Length > 1 && char.IsUpper(controllerName[1]))
        {
            controllerName = controllerName[1..];
        }
        controller.ControllerName = controllerName
            .RemoveSuffix("Proxy")
            .RemoveSuffix("Controller")
            .RemoveSuffix("ApplicationService");
    }

    private static void SetGroupName(ControllerModel controller)
    {
        if (!string.IsNullOrEmpty(controller.ApiExplorer.GroupName))
        {
            return;
        }
        controller.ApiExplorer.GroupName = controller.ControllerName.Pluralize();
    }

    private static void SetVisibility(ControllerModel controller)
    {
        if (controller.ApiExplorer.IsVisible.HasValue)
        {
            return;
        }
        controller.ApiExplorer.IsVisible = true;
    }

    private static bool IsVisible(IApiExplorerModel controller)
    {
        return controller.ApiExplorer.IsVisible ?? true;
    }

    private static void ConfigureRoute(ControllerModel controller)
    {
        if (!controller.Selectors.Any())
        {
            controller.Selectors.Add(new SelectorModel());
        }

        foreach (var selector in controller.Selectors)
        {
            selector.AttributeRouteModel ??= new AttributeRouteModel(new RouteAttribute("api"));
        }
    }

    private static void ConfigureAction(ActionModel action)
    {
        NormalizeName(action);
        SetVisibility(action);
        if (!IsVisible(action))
        {
            return;
        }
        ConfigureRoute(action);
        ConfigureParameters(action);
    }

    private static void NormalizeName(ActionModel action)
    {
        string actionName = action.ActionName.RemoveSuffix("Async");
        action.ActionName = actionName;
    }

    private static void SetVisibility(ActionModel action)
    {
        if (action.ApiExplorer.IsVisible.HasValue)
        {
            return;
        }

        string actionName = action.ActionName;
        action.ApiExplorer.IsVisible = actionName != "DynProxyGetTarget"
            && actionName != "DynProxySetTarget"
            && actionName != "GetInterceptors"; ;
    }

    private static void ConfigureRoute(ActionModel action)
    {
        if (!action.Selectors.Any())
        {
            action.Selectors.Add(new SelectorModel());
        }

        foreach (var selector in action.Selectors)
        {
            string httpMethod = selector.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods?.FirstOrDefault()
                ?? GetHttpMethodByActionName(action);

            if (selector.AttributeRouteModel is null)
            {
                string routeUrlTemplate = GetRouteUrlTemplate(action);
                string routeName = GetRouteName(action);
                selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(routeUrlTemplate) { Name = routeName });
            }

            if (!selector.ActionConstraints.OfType<HttpMethodActionConstraint>().Any())
            {
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { httpMethod }));
            }
        }
    }

    private static string GetHttpMethodByActionName(ActionModel action)
    {
        string actionName = action.ActionName;
        string? actionPrefix = actionName.FindPrefix(_prefixes);
        return actionPrefix switch
        {
            "Get" => "GET",
            "Add" or "Create" => "POST",
            "Update" => "PUT",
            "Delete" or "Remove" => "DELETE",
            _ => "POST",
        };
    }

    private static string GetRouteUrlTemplate(ActionModel action)
    {
        string actionUrlTemplate = action.Controller.ControllerName.Pluralize();

        List<ParameterModel> idParameters = action.Parameters
                .Where(x => x.ParameterName is not null
                    && (x.ParameterName == "id" || x.ParameterName.EndsWith("Id", StringComparison.Ordinal))
                    && IsPrimitiveType(x.ParameterType))
                .ToList();
        if (idParameters.Any())
        {
            actionUrlTemplate += $"/{{{idParameters[0].ParameterName}}}";
        }

        string actionName = action.ActionName;
        string? actionPrefix = actionName.FindPrefix(_prefixes);
        actionName = actionName
            .RemovePrefix(actionPrefix)
            .RemoveSuffix("List");

        if (!actionName.IsNullOrEmpty())
        {
            if (actionPrefix is not null)
            {
                actionName = actionName.Pluralize();
            }

            actionUrlTemplate += $"/{actionName}";
            if (idParameters.Count > 1)
            {
                actionUrlTemplate += $"/{{{idParameters[1].ParameterName}}}";
            }
        }

        return actionUrlTemplate;
    }

    private static string GetRouteName(ActionModel action)
    {
        string actionName = action.ActionName;

        string? actionPrefix = actionName.FindPrefix(_prefixes);
        actionName = actionName.RemovePrefix(actionPrefix);

        string controllerName = action.Controller.ControllerName;
        if (!actionName.StartsWith(controllerName))
        {
            actionName = controllerName + actionName;
        }

        return $"{actionPrefix}{actionName}";
    }

    private static void ConfigureParameters(ActionModel action)
    {
        var httpMethods = action.Selectors
            .SelectMany(x => x.ActionConstraints.OfType<HttpMethodActionConstraint>())?
            .SelectMany(x => x.HttpMethods) ?? Array.Empty<string>();

        bool useFormBodyBinding = !httpMethods.Contains("GET")
            && !httpMethods.Contains("DELETE");

        foreach (ParameterModel parameter in action.Parameters)
        {
            if (useFormBodyBinding
                && !IsPrimitiveType(parameter.ParameterInfo.ParameterType)
                && !typeof(CancellationToken).IsAssignableFrom(parameter.ParameterInfo.ParameterType))
            {
                parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
            }
        }
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