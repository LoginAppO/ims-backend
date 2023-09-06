using Ims.Application.Pagination;

namespace Ims.Application.Logins;

public class GetLoginsQueryHandler : IRequestHandler<GetLoginsQuery, ApplicationResponse<PaginationInfo<ListLoginsResponse>>>
{
    private readonly ILoginRepository _loginRepository;
    private readonly IPaginationRepository<ListLoginsResponse> _paginationRepository;

    public GetLoginsQueryHandler(ILoginRepository loginRepository, IPaginationRepository<ListLoginsResponse> paginationRepository)
    {
        _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        _paginationRepository = paginationRepository ?? throw new ArgumentNullException(nameof(paginationRepository));
    }

    public async Task<ApplicationResponse<PaginationInfo<ListLoginsResponse>>> Handle(
        GetLoginsQuery request,
        CancellationToken cancellationToken)
    {
        var loginsQuery = _loginRepository.GetQueryableLogins();

        var result = loginsQuery
        .Select(x => new ListLoginsResponse
        {
            Id = x.Id,
            Email = x.Email,
            LoggedAt = x.LoggedAt,
            Status = x.Status,
        });

        var logins = await new Pagination<ListLoginsResponse>(_paginationRepository)
            .CreateAsync(
            result,
            request.PageNumber,
            request.PageSize);

        return logins;
    }
}
