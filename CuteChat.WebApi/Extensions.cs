using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CuteChat.WebApi;
public static class Extensions
{
    public static int? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid)?.Value;
        if (userId is null)
            return null;

        if (!int.TryParse(userId, out var id))
            return null;

        return id;
    }
    
}
