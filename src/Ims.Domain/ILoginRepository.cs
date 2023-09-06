namespace Ims.Domain;

public interface ILoginRepository
{
    Task<IReadOnlyCollection<Login>> List();

    IQueryable<Login> GetQueryableLogins();

    Task<Login> Add(Login login);

    IQueryable<Login> Filter(string filter);
}
