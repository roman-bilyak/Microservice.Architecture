namespace Microservice.Application;

public abstract class ListDto<T>
{
    public List<T> Items { get; protected set; } = new List<T>();

    public long TotalCount { get; set; }
}