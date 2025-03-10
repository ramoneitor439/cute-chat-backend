using CuteChat.Domain.Entities;
using CuteChat.Infrastructure.Pagination;
using CuteChat.Infrastructure.Results;

namespace CuteChat.Services;

public interface IReadService<T> where T : BaseEntity
{
    BaseResult<T?> GetById(object id, bool asTracking = false);
    Task<BaseResult<T?>> GetByIdAsync(object id, bool asTracking = false);
    BaseResult<PaginatedResult<T>> GetAll(IBaseReadRequest request, bool asTracking = false);
    Task<BaseResult<PaginatedResult<T>>> GetAllAsync(IBaseReadRequest request, bool asTracking = false);
}
