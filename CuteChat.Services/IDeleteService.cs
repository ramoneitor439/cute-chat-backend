using CuteChat.Domain.Entities;
using CuteChat.Infrastructure.Results;

namespace CuteChat.Services;

public interface IDeleteService<T> where T : BaseEntity
{
    BaseResult<Empty> Remove(T entity);
    BaseResult<Empty> RemoveById(object id);
    BaseResult<Empty> RemoveRange(params T[] entities);
    BaseResult<Empty> RemoveRange(IEnumerable<T> entities);
    BaseResult<Empty> RemoveRangeByIds(params object[] ids);
    BaseResult<Empty> RemoveRangeByIds(IEnumerable<object> ids);
    Task<BaseResult<Empty>> RemoveAsync(T entity);
    Task<BaseResult<Empty>> RemoveByIdAsync(object id);
    Task<BaseResult<Empty>> RemoveRangeAsync(params T[] entities);
    Task<BaseResult<Empty>> RemoveRangeAsync(IEnumerable<T> entities);
    Task<BaseResult<Empty>> RemoveRangeByIdsAsync(params object[] ids);
    Task<BaseResult<Empty>> RemoveRangeByIdsAsync(IEnumerable<object> ids);

}
