namespace Microservice.Application;

public abstract class ListQuery<TResponse> : Query<TResponse>
    where TResponse : class
{
    public int PageIndex { get; protected set; }
    public int PageSize { get; protected set; }

    protected ListQuery(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}