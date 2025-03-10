namespace CuteChat.Infrastructure.Pagination;
public class PaginatedResult<T> where T : class
{
    public int TotalPages { get; set; }
    public int TotalData { get; set; }
    public IEnumerable<T> Data { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
}
