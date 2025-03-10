

namespace CuteChat.Domain.Entities;

public class Contact : BaseEntity, IAuditable
{
    public int ContactUserId { get; set; }
    public virtual AppUser? ContactUser { get; set; }
    public int UserId { get; set; }
    public virtual AppUser? User { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Contact(int userId, int contactUserId)
    {
        ContactUserId = contactUserId;
        UserId = userId;
    }
}
