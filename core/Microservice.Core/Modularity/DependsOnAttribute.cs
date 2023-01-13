namespace Microservice.Core.Modularity;

public abstract class DependsOnAttribute : Attribute
{
    public Type Type { get; }

    public DependsOnAttribute(Type type)
    {
        if (type.IsAssignableFrom(typeof(IStartupModule))
            && type.GetConstructor(Type.EmptyTypes) is not null)
        {
            throw new ArgumentException("Invalid type", nameof(type));
        }
        Type = type;
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute<T> : DependsOnAttribute
    where T : class, IStartupModule, new()
{
    public DependsOnAttribute()
        :base(typeof(T))
    {
    }
}