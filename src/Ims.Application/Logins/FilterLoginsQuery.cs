using Ims.Application.Pagination;

namespace Ims.Application.Logins;

public class FilterLoginsQuery : IRequest<ApplicationResponse<PaginationInfo<ListLoginsResponse>>>
{
    public string? SearchQuery { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
