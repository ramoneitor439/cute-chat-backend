namespace CuteChat.Models.AppUser.Message;

public class ListMessageResponse
{
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public bool Incoming { get; set; }
    public string Hours { get; set; } = string.Empty;
    public string Minutes { get; set; } = string.Empty;
}
