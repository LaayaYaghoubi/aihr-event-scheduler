using Microsoft.EntityFrameworkCore;

namespace AIHR.EventSchedulerInfrastructure.Helper;

public class PagedList<T>(List<T> items, int page, int pageSize, int totalCount)
{
    public List<T> Items { get; } = items;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => PageSize > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, Pagination pagination)
    {
        var totalCount = await query.CountAsync();
        var currentPage = pagination.Page ?? 1;
        var currentPageSize = pagination.PageSize ?? totalCount;

        var items = await query.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize).ToListAsync();
        return new PagedList<T>(items, currentPage, currentPageSize, totalCount);
    }
}