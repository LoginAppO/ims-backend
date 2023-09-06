using Ims.Application.Pagination;

namespace Ims.Application.Users;

public class GetUsersQuery : IRequest<ApplicationResponse<PaginationInfo<ListUsersResponse>>>
{
    public GetUsersQuery(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }
}
