namespace CuteChat.Infrastructure.Security;
public interface IJwtService
{
    string CreateAccessToken(Domain.Entities.AppUser user);
}
