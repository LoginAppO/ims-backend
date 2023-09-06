namespace Ims.Application.Pagination;

public class PaginationInfo<T>
{
    public PaginationInfo(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Items { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasNextPage => PageNumber * PageSize < TotalCount;

    public bool HasPreviousPage => PageNumber > 1;
}
