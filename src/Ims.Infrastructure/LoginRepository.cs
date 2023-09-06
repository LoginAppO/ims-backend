using Ims.Domain;

namespace Ims.Infrastructure;

public class LoginRepository : ILoginRepository
{
    private readonly ImsDbContext _imsDbContext;

    public LoginRepository(ImsDbContext imsDbContext)
    {
        _imsDbContext = imsDbContext;
    }

    public async Task<IReadOnlyCollection<Login>> List()
    {
        return await _imsDbContext.Logins
            .OrderByDescending(login => login.LoggedAt)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public IQueryable<Login> GetQueryableLogins()
    {
        return _imsDbContext.Logins
            .OrderByDescending(login => login.LoggedAt)
            .AsQueryable();
    }

    public async Task<Login> Add(Login login)
    {
        var newLogin = await _imsDbContext.AddAsync(login).ConfigureAwait(false);

        _ = await _imsDbContext.SaveChangesAsync().ConfigureAwait(false);

        return newLogin.Entity;
    }

    public IQueryable<Login> Filter(string filter)
    {
        var requestsQuery = _imsDbContext.Logins
            .OrderByDescending(login => login.LoggedAt)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            requestsQuery = requestsQuery.Where(l => l.Email.ToLower().Contains(filter.ToLower()));
        }

        return requestsQuery;
    }
}
