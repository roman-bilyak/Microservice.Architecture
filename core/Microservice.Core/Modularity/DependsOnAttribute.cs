namespace Microservice.Core.Modularity;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute : Attribute
{
    public Type Type { get; }

    public DependsOnAttribute(Type type)
    {
        Type = type;
    }
}