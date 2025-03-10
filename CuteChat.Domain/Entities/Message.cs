
namespace CuteChat.Domain.Entities;

public class Message : BaseEntity, IAuditable
{
    public string Content { get; set; } = string.Empty;
    public int SenderId { get; set; }
    public virtual AppUser? Sender { get; set; }
    public int ReceiverId { get; set; }
    public virtual AppUser? Receiver { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Message(int senderId, int receiverId, string content)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
    }
}
