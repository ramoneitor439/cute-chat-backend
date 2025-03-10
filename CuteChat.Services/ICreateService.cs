using CuteChat.Domain.Entities;
using CuteChat.Infrastructure.Results;

namespace CuteChat.Services;
public interface ICreateService<T> where T : BaseEntity
{
    BaseResult<object> Create(T entity);
    Task<BaseResult<object>> CreateAsync(T entity);
    BaseResult<Empty> CreateRange(params T[] entities);
    BaseResult<Empty> CreateRange(IEnumerable<T> entities);
    Task<BaseResult<Empty>> CreateRangeAsync(params T[] entities);
    Task<BaseResult<Empty>> CreateRangeAsync(IEnumerable<T> entities);
}
