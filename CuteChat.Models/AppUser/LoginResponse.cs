using System.ComponentModel.DataAnnotations;

namespace CuteChat.Models.AppUser;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public int Expires { get; set; }
}
