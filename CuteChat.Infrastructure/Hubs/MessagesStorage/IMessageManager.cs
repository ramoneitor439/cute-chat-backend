namespace CuteChat.Infrastructure.Hubs.MessagesStorage;

public interface IMessageManager
{
    public Task SaveMessageAsync(int senderId, int receiverId, string content);
}
