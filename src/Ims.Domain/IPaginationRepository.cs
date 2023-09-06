namespace Ims.Domain;

public interface IPaginationRepository<T>
{
    Task<int> CountAsync(IQueryable<T> query);

    Task<List<T>> ToListAsync(IQueryable<T> query);
}
