using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Xml;

namespace Microservice.AspNetCore.Swagger;

internal class InheritDocOperationFilter : IOperationFilter
{
    private static readonly Dictionary<string, XmlDocument> XmlCommentsCache = new();

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        MethodInfo methodInfo = context.MethodInfo;

        if (operation.Summary is null)
        {
            string summary = GetSummary(methodInfo);
            if (!string.IsNullOrEmpty(summary))
            {
                operation.Summary = summary;
            }
        }

        ParameterInfo[] parameters = methodInfo.GetParameters();
        foreach (OpenApiParameter parameter in operation.Parameters)
        {
            if (parameter.Description is not null)
            {
                continue;
            }

            ParameterInfo? parameterInfo = parameters.FirstOrDefault(x => x.Name == parameter.Name);
            if (parameterInfo is not null)
            {
                string parameterSummary = GetParameterSummary(parameterInfo);
                if (!string.IsNullOrEmpty(parameterSummary))
                {
                    parameter.Description = parameterSummary;
                }
            }
        }
    }

    private static string GetSummary(MethodInfo methodInfo)
    {
        string summary = GetSummaryFromXmlComments(methodInfo);
        if (string.IsNullOrEmpty(summary))
        {
            summary = GetSummaryFromInheritedMethod(methodInfo);
        }
        return summary;
    }

    private static string GetParameterSummary(ParameterInfo parameterInfo)
    {
        string summary = GetSummaryFromXmlComments(parameterInfo.Member);
        if (string.IsNullOrEmpty(summary))
        {
            summary = GetSummaryFromInheritedParameter(parameterInfo);
        }
        return summary;
    }

    private static string GetSummaryFromXmlComments(MemberInfo memberInfo)
    {
        Type? declaringType = memberInfo.DeclaringType;
        if (declaringType is null)
        {
            return string.Empty;
        }

        Assembly declaringAssembly = declaringType.Assembly;
        string xmlCommentsPath = Path.ChangeExtension(declaringAssembly.Location, ".xml");
        if (!XmlCommentsCache.TryGetValue(xmlCommentsPath, out XmlDocument? xmlComments))
        {
            xmlComments = new XmlDocument();
            try
            {
                xmlComments.Load(xmlCommentsPath);
            }
            catch
            {
            }

            XmlCommentsCache[xmlCommentsPath] = xmlComments;
        }

        XmlNode? memberNode = xmlComments.SelectSingleNode($"//member[@name='{GetMemberName(memberInfo)}']");
        if (memberNode is not null)
        {
            XmlNode? summaryNode = memberNode.SelectSingleNode("summary");
            if (summaryNode is not null)
            {
                return summaryNode.InnerText.Trim();
            }
        }

        return string.Empty;
    }

    private static string GetSummaryFromInheritedMethod(MethodInfo methodInfo)
    {
        MethodInfo baseMethodInfo = methodInfo.GetBaseDefinition();
        if (baseMethodInfo is not null && baseMethodInfo != methodInfo)
        {
            return GetSummary(baseMethodInfo);
        }

        if (methodInfo.DeclaringType is null)
        {
            return string.Empty;
        }

        foreach (Type interfaceType in methodInfo.DeclaringType.GetInterfaces())
        {
            MethodInfo? interfaceMethodInfo = interfaceType.GetMethod(methodInfo.Name, methodInfo.GetParameters().Select(p => p.ParameterType).ToArray());
            if (interfaceMethodInfo is not null)
            {
                string interfaceSummary = GetSummary(interfaceMethodInfo);
                if (!string.IsNullOrEmpty(interfaceSummary))
                {
                    return interfaceSummary;
                }
            }
        }

        return string.Empty;
    }

    private static string GetSummaryFromInheritedParameter(ParameterInfo parameterInfo)
    {
        MemberInfo memberInfo = parameterInfo.Member;

        Type? declaringType = memberInfo.DeclaringType;
        if (declaringType is null)
        {
            return string.Empty;
        }

        if (memberInfo is not MethodInfo declaringMethodInfo)
        {
            return string.Empty;
        }

        MethodInfo baseMethodInfo = declaringMethodInfo.GetBaseDefinition();
        if (baseMethodInfo is null || baseMethodInfo == declaringMethodInfo)
        {
            return string.Empty;
        }

        ParameterInfo? baseParameterInfo = baseMethodInfo.GetParameters()
            .FirstOrDefault(x => x.Name == parameterInfo.Name);

        if (baseParameterInfo is null)
        {
            return string.Empty;
        }

        return GetParameterSummary(baseParameterInfo);
    }

    private static string GetMemberName(MemberInfo memberInfo)
    {
        if (memberInfo is Type type)
        {
            return $"T:{type.FullName}";
        }
        if (memberInfo is MethodInfo methodInfo)
        {
            return $"M:{methodInfo.DeclaringType?.FullName}.{methodInfo.Name}({string.Join(",", methodInfo.GetParameters().Select(x => x.ParameterType.FullName))})";
        }
        if (memberInfo is PropertyInfo propertyInfo)
        {
            return $"P:{propertyInfo.DeclaringType?.FullName}.{propertyInfo.Name}";
        }
        if (memberInfo is FieldInfo fieldInfo)
        {
            return $"F:{fieldInfo.DeclaringType?.FullName}.{fieldInfo.Name}";
        }
        if (memberInfo is EventInfo eventInfo)
        {
            return $"E:{eventInfo.DeclaringType?.FullName}.{eventInfo.Name}";
        }
        if (memberInfo is ConstructorInfo constructorInfo)
        {
            return $"M:{constructorInfo.DeclaringType?.FullName}.#ctor";
        }

        throw new ArgumentException($"Cannot generate member name for type {memberInfo.GetType().FullName}");
    }
}