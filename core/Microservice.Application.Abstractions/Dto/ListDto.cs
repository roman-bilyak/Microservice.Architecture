namespace Microservice.Application;

public abstract record ListDto<T>
{
    public List<T> Items { get; init; } = new List<T>();

    public long TotalCount { get; init; }
}