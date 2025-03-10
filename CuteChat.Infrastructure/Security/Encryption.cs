using System.Security.Cryptography;
using System.Text;

namespace CuteChat.Infrastructure.Security;

public static class Encryption
{
    public static string Hash(string source, string secretKey)
    {
        var combined = $"{source}:{secretKey}";
        var result = SHA256.HashData(Encoding.UTF8.GetBytes(combined));
        return Convert.ToBase64String(result);
    }

    public static bool Compare(string original, string hashed, string secretKey)
    {
        var originalHash = Hash(original, secretKey);
        return originalHash == hashed;
    }
}
