namespace CuteChat.Models.Contact;
public class ContactResponse
{
    public int ContactUserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime LastConnection { get; set; }
    public string? LastMessage { get; set; } = string.Empty;
}
