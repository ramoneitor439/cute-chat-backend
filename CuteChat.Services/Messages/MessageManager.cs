using CuteChat.Domain.Entities;
using CuteChat.Infrastructure.Results;
using CuteChat.Models.AppUser.Message;
using CuteChat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CuteChat.Services.Messages;

public class MessageManager(ApplicationContext context) : BaseEntityService<Message>(context)
{
    public async Task<BaseResult<IEnumerable<ListMessageResponse>>> GetAllByUserAsync(int userId, int contactUserId)
    {
        var messages = await _repository
            .AsNoTracking()
            .Where(x => x.ReceiverId == userId || x.ReceiverId == contactUserId)
            .Where(x => x.SenderId == userId || x.SenderId == contactUserId)
            .Include(x => x.Sender)
            .OrderBy(x => x.CreatedAt)
            .Select(x => new ListMessageResponse
            {
                Content = x.Content,
                From = x.SenderId.ToString(),
                Incoming = x.SenderId != userId,
                Hours = x.CreatedAt.Hour.ToString(),
                Minutes = x.CreatedAt.Minute.ToString(),
                Name = x.Sender!.FullName
            })
            .ToListAsync();

        return Result.Success(messages.AsEnumerable());
    }
}
