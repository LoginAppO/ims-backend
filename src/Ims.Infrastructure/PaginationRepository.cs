using Ims.Domain;

namespace Ims.Infrastructure;

public class PaginationRepository<T> : IPaginationRepository<T>
{
    public async Task<int> CountAsync(IQueryable<T> query)
    {
        return await query.AsQueryable().CountAsync();
    }

    public async Task<List<T>> ToListAsync(IQueryable<T> query)
    {
        return await query.ToListAsync();
    }
}
