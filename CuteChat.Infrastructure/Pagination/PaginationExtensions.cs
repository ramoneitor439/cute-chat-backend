using Microsoft.EntityFrameworkCore;

namespace CuteChat.Infrastructure.Pagination;

public static class PaginationExtensions
{
    public static PaginatedResult<T> ToPaginated<T>(this IQueryable<T> source, int page, int pageSize) where T : class
    {
        var totalElements = source.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalElements / pageSize);

        var data = source
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToArray();

        return new PaginatedResult<T>
        {
            Data = data,
            Page = page,
            PageSize = pageSize,
            TotalData = totalElements,
            TotalPages = totalPages
        };
    }

    public static async Task<PaginatedResult<T>> ToPaginatedAsync<T>(this IQueryable<T> source, int page, int pageSize) where T : class
    {
        var totalElements = await source.CountAsync();

        var totalPages = (int)Math.Ceiling((decimal)totalElements / pageSize);

        var data = await source
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToArrayAsync();
            
        return new PaginatedResult<T>
        {
            Data = data,
            Page = page,
            PageSize = pageSize,
            TotalData = totalElements,
            TotalPages = totalPages
        };
    }
}
