namespace Ims.Domain;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> List();

    IQueryable<User> GetQueryableUsers();

    Task<bool> Any(string email);

    Task<User> Add(User user);

    IQueryable<User> Filter(string filter);

    Task<User> GetByEmail(string email);
}
