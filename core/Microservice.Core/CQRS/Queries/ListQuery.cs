namespace Microservice.Core.CQRS.Queries
{
    public abstract class ListQuery<TResponse> : IQuery<TResponse>
        where TResponse : notnull
    {
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
    }
}