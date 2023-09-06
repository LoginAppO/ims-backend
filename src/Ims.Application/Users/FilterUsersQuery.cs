using Ims.Application.Pagination;

namespace Ims.Application.Users;

public class FilterUsersQuery : IRequest<ApplicationResponse<PaginationInfo<ListUsersResponse>>>
{
    public string? SearchQuery { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
