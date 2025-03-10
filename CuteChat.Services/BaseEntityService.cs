using CuteChat.Domain.Entities;
using CuteChat.Infrastructure.Pagination;
using CuteChat.Infrastructure.Results;
using CuteChat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CuteChat.Services;

public abstract class BaseEntityService<T>(ApplicationContext context) : ICreateService<T>, IReadService<T>, IDeleteService<T>, IUpdateService<T> where T : BaseEntity
{
    protected readonly DbSet<T> _repository = context.Set<T>();

    #region Create
    public virtual BaseResult<object> Create(T entity)
    {
        try
        {
            _repository.Add(entity);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return Result.Error<object>("Error creating entity", $"{ex.Message} | {ex.StackTrace}");
        }
        return Result.Success((object)entity.Id);
    }

    public virtual async Task<BaseResult<object>> CreateAsync(T entity)
    {
        try
        {
            _repository.Add(entity);
            await context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            return Result.Error<object>("Error creating entity", $"{ex.Message} | {ex.StackTrace}");
        }
        return Result.Success((object)entity.Id);
    }

    public virtual BaseResult<Empty> CreateRange(params T[] entities)
    {
        try
        {
            _repository.AddRange(entities);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return Result.Error("Error creating entities", $"{ex.Message} | {ex.StackTrace}");
        }
        return Result.Success();
    }

    public virtual BaseResult<Empty> CreateRange(IEnumerable<T> entities)
    {
        try
        {
            _repository.AddRange(entities);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return Result.Error("Error creating entities", $"{ex.Message} | {ex.StackTrace}");
        }

        return Result.Success();
    }

    public virtual async Task<BaseResult<Empty>> CreateRangeAsync(params T[] entities)
    {
        try
        {
            _repository.AddRange(entities);
            await context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            return Result.Error("Error creating entities", $"{ex.Message} | {ex.StackTrace}");
        }
        return Result.Success();
    }

    public virtual async Task<BaseResult<Empty>> CreateRangeAsync(IEnumerable<T> entities)
    {
        try
        {
            _repository.AddRange(entities);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Error("Error creating entities", $"{ex.Message} | {ex.StackTrace}");
        }
        return Result.Success();
    }
    #endregion

    #region Read
    public virtual BaseResult<PaginatedResult<T>> GetAll(IBaseReadRequest request, bool asTracking = false)
    {
        try
        {
            var query = asTracking ? _repository.AsTracking() : _repository.AsNoTracking();
            var result = query.ToPaginated(request.Page, request.PageSize);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error<PaginatedResult<T>>("Error getting data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public virtual async Task<BaseResult<PaginatedResult<T>>> GetAllAsync(IBaseReadRequest request, bool asTracking = false)
    {
        try
        {
            var query = asTracking ? _repository.AsTracking() : _repository.AsNoTracking();
            var result = await query.ToPaginatedAsync(request.Page, request.PageSize);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error<PaginatedResult<T>>("Error getting data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public virtual BaseResult<T?> GetById(object id, bool asTracking = false)
    {
        try
        {
            var query = asTracking ? _repository.AsTracking() : _repository.AsNoTracking();
            var result = query.FirstOrDefault(x => x.Id == (int)id);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error<T?>("Error getting data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public virtual async Task<BaseResult<T?>> GetByIdAsync(object id, bool asTracking = false)
    {
        try
        {
            var query = asTracking ? _repository.AsTracking() : _repository.AsNoTracking();
            var result = await query.FirstOrDefaultAsync(x => x.Id == (int)id);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error<T?>("Error getting data", $"{ex.Message} | {ex.StackTrace}");
        }
    }
    #endregion

    #region Delete
    public BaseResult<Empty> Remove(T entity)
    {
        try
        {
            _repository.Remove(entity);
            context.SaveChanges();

            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public async Task<BaseResult<Empty>> RemoveAsync(T entity)
    {
        try
        {
            _repository.Remove(entity);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public BaseResult<Empty> RemoveById(object id)
    {
        try
        {
            var entityResult = GetById(id, asTracking: true);
            if (!entityResult.IsSuccess || entityResult.Data is null)
                throw new Exception($"Error getting entity: {string.Join('\n', entityResult.Errors.Select(x => x.Description))}");
            
            _repository.Remove(entityResult.Data);
            context.SaveChanges();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public async Task<BaseResult<Empty>> RemoveByIdAsync(object id)
    {
        try
        {
            var entityResult = await GetByIdAsync(id, asTracking: true);
            if (!entityResult.IsSuccess || entityResult.Data is null)
                throw new Exception($"Error getting entity: {string.Join('\n', entityResult.Errors.Select(x => x.Description))}");

            _repository.Remove(entityResult.Data);
            context.SaveChanges();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public BaseResult<Empty> RemoveRange(params T[] entities)
    {
        try
        {
            _repository.RemoveRange(entities);
            context.SaveChanges();

            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public BaseResult<Empty> RemoveRange(IEnumerable<T> entities)
    {
        try
        {
            _repository.RemoveRange(entities);
            context.SaveChanges();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public async Task<BaseResult<Empty>> RemoveRangeAsync(params T[] entities)
    {
        try
        {
            _repository.RemoveRange(entities);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public async Task<BaseResult<Empty>> RemoveRangeAsync(IEnumerable<T> entities)
    {
        try
        {
            _repository.RemoveRange(entities);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public BaseResult<Empty> RemoveRangeByIds(params object[] ids)
    {
        try
        {
            var entities = _repository.Where(x => ids.Contains(x.Id)).ToArray();
            _repository.RemoveRange(entities);
            context.SaveChanges();

            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public BaseResult<Empty> RemoveRangeByIds(IEnumerable<object> ids)
    {
        try
        {
            var entities = _repository.Where(x => ids.Contains(x.Id)).ToArray();
            _repository.RemoveRange(entities);
            context.SaveChanges();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public async Task<BaseResult<Empty>> RemoveRangeByIdsAsync(params object[] ids)
    {
        try
        {
            var entities = await _repository.Where(x => ids.Contains(x.Id)).ToArrayAsync();
            _repository.RemoveRange(entities);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public async Task<BaseResult<Empty>> RemoveRangeByIdsAsync(IEnumerable<object> ids)
    {
        try
        {
            var entities = await _repository.Where(x => ids.Contains(x.Id)).ToArrayAsync();
            _repository.RemoveRange(entities);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }
    #endregion

    #region Update
    public object Update(object id, T entity)
    {
        try
        {
            var entityResult = GetById(id, asTracking: true);
            if (!entityResult.IsSuccess || entityResult.Data is null)
                throw new Exception($"Error getting entity: {string.Join('\n', entityResult!.Errors.Select(x => x.Description))}");

            _repository.Entry(entityResult.Data).CurrentValues.SetValues(entity);
            _repository.Entry(entityResult.Data).Property(x => x.Id).IsModified = false;

            if(entityResult.Data is IAuditable auditable)
            {
                context.Entry(auditable).Property(x => x.CreatedAt).IsModified = false;
                context.Entry(auditable).Property(x => x.UpdatedAt).IsModified = false;
            }

            context.SaveChanges();

            return Result.Success<object>(entityResult.Data.Id);
        }
        catch(Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }

    public async Task<object> UpdateAsync(object id, T entity)
    {
        try
        {
            var entityResult = await GetByIdAsync(id, asTracking: true);
            if (!entityResult.IsSuccess || entityResult.Data is null)
                throw new Exception($"Error getting entity: {string.Join('\n', entityResult!.Errors.Select(x => x.Description))}");

            _repository.Entry(entityResult.Data).CurrentValues.SetValues(entity);
            _repository.Entry(entityResult.Data).Property(x => x.Id).IsModified = false;

            if (entityResult.Data is IAuditable auditable)
            {
                context.Entry(auditable).Property(x => x.CreatedAt).IsModified = false;
                context.Entry(auditable).Property(x => x.UpdatedAt).IsModified = false;
            }

            await context.SaveChangesAsync();

            return Result.Success<object>(entityResult.Data.Id);
        }
        catch (Exception ex)
        {
            return Result.Error("Error removing data", $"{ex.Message} | {ex.StackTrace}");
        }
    }
    #endregion
}
