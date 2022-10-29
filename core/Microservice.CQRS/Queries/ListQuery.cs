namespace Microservice.CQRS.Queries;

public abstract class ListQuery : IQuery
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
}