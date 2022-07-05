namespace Microservice.Core.Application.Dto;

public abstract class ListDto<T>
{
    public List<T> Items { get; set; } = new List<T>();

    public long TotalCount { get; set; }
}