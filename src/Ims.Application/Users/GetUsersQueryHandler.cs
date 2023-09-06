using Ims.Application.Pagination;

namespace Ims.Application.Users;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApplicationResponse<PaginationInfo<ListUsersResponse>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPaginationRepository<ListUsersResponse> _paginationRepository;

    public GetUsersQueryHandler(IUserRepository userRepository, IPaginationRepository<ListUsersResponse> paginationRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _paginationRepository = paginationRepository ?? throw new ArgumentNullException(nameof(paginationRepository));
    }

    public async Task<ApplicationResponse<PaginationInfo<ListUsersResponse>>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var usersQuery = _userRepository.GetQueryableUsers();

        var result = usersQuery
        .Select(x => new ListUsersResponse
        {
            Id = x.Id,
            Email = x.Email,
        });

        var users = await new Pagination<ListUsersResponse>(_paginationRepository)
            .CreateAsync(
            result,
            request.PageNumber,
            request.PageSize);

        return users;
    }
}
