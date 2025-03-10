namespace CuteChat.Domain.Entities;

public class AppUser : BaseEntity, IAuditable
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName}{(string.IsNullOrEmpty(MiddleName) ? string.Empty : " " + MiddleName)} {LastName}";
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password = string.Empty;
    public virtual ICollection<Contact>? Contacts { get; set; }
    public virtual ICollection<Message>? IncomingMessages { get; set; }
    public virtual ICollection<Message>? SentMessages { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
