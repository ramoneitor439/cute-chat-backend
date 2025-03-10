using Riok.Mapperly.Abstractions;

namespace CuteChat.Models.AppUser.Mappers;

[Mapper]
public static partial class AppUserMapper
{
    public static partial Domain.Entities.AppUser ToBaseEntity(this RegisterUserRequest source);
    public static partial UserInfoResponse ToUserInfo(this Domain.Entities.AppUser source);
}
