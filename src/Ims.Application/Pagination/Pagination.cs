namespace Ims.Application.Pagination;

public class Pagination<T>
{
    private readonly IPaginationRepository<T> _repository;

    public Pagination(IPaginationRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ApplicationResponse<PaginationInfo<T>>> CreateAsync(IQueryable<T> query, int pageNumber, int pageSize)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return ApplicationResponse
                .BadRequest<PaginationInfo<T>>("Page number and size cannot be smaller than 1.");
        }

        var totalCount = await _repository.CountAsync(query);

        var items = await _repository.ToListAsync(query.Skip((pageNumber - 1) * pageSize).Take(pageSize));

        return ApplicationResponse.Success(new PaginationInfo<T>(items, pageNumber, pageSize, totalCount));
    }
}
