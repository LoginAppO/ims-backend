using Ims.Domain;
using Ims.Domain.Entities;

namespace Ims.Infrastructure;

public class UserRepository : IUserRepository
{

    private readonly ImsDbContext _imsDbContext;

    public UserRepository(ImsDbContext imsDbContext)
    {
        _imsDbContext = imsDbContext;
    }

    public async Task<IReadOnlyCollection<User>> List()
    {
        return await _imsDbContext.Users.ToListAsync().ConfigureAwait(false);
    }

    public IQueryable<User> GetQueryableUsers()
    {
        return _imsDbContext.Users.AsQueryable();
    }

    public async Task<bool> Any(string email)
    {
        return await _imsDbContext.Users.AnyAsync(u => u.Email == email).ConfigureAwait(false);
    }

    public async Task<User> Add(User user)
    {
        var newUser = await _imsDbContext.AddAsync(user).ConfigureAwait(false);

        _ = await _imsDbContext.SaveChangesAsync().ConfigureAwait(false);

        return newUser.Entity;
    }

    public IQueryable<User> Filter(string filter)
    {
        var requestsQuery = _imsDbContext.Users.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            requestsQuery = requestsQuery.Where(u => u.Email.ToLower().Contains(filter.ToLower()));
        }

        return requestsQuery;
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _imsDbContext.Users.FirstOrDefaultAsync<User>(u => u.Email == email);
    }
}
