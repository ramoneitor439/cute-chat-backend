using CuteChat.Domain.Entities;

namespace CuteChat.Services;

public interface IUpdateService<T> where T : BaseEntity
{
    object Update(object id, T entity);
    Task<object> UpdateAsync(object id, T entity);
}
