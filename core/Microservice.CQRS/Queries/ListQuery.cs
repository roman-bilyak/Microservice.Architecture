namespace Microservice.CQRS;

public abstract class ListQuery : Query
{
    public int PageIndex { get; protected set; }
    public int PageSize { get; protected set; }

    protected ListQuery(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}