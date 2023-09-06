using Ims.Application.Pagination;

namespace Ims.Application.Logins;

public class GetLoginsQuery : IRequest<ApplicationResponse<PaginationInfo<ListLoginsResponse>>>
{
    public GetLoginsQuery(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }
}
