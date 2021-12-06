namespace Microservice.Core;

public class ObjectWrapper<T>
{
    public T Object { get; set; }

    public ObjectWrapper()
    {
    }

    public ObjectWrapper(T @object) : this()
    {
        ArgumentNullException.ThrowIfNull(@object);

        Object = @object;
    }
}