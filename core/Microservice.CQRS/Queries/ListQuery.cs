namespace Microservice.CQRS;

public abstract class ListQuery : IQuery
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
}