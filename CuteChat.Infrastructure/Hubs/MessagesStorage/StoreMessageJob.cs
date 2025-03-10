using CuteChat.Domain.Entities;
using CuteChat.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CuteChat.Infrastructure.Hubs.MessagesStorage;

public class StoreMessageJob(IServiceProvider serviceProvider) : IMessageManager
{
    public async Task SaveMessageAsync(int senderId, int receiverId, string content)
    {
        var context = serviceProvider.GetRequiredService<ApplicationContext>();
        if (context is null)
            return;

        var messageRepository = context.Set<Message>();
        messageRepository.Add(new Message(senderId, receiverId, content));
        await context.SaveChangesAsync();
    }
}
