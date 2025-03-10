using CuteChat.Infrastructure.Results;
using CuteChat.Infrastructure.Security;
using CuteChat.Models.AppUser;
using CuteChat.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CuteChat.Services.AppUser;
public class AppUserManager(
    ApplicationContext context,
    IJwtService jwtService,
    IOptions<AccountSettings> options) : BaseEntityService<Domain.Entities.AppUser>(context)
{
    private readonly AccountSettings _settings = options.Value;

    public override async Task<BaseResult<object>> CreateAsync(Domain.Entities.AppUser entity)
    {
        try
        {
            var userExists = await _repository.AsNoTracking().AnyAsync(x => x.Email == entity.Email);
            if (userExists)
                return Result.Error<object>("Error creating user", "Email already exists", 409);

            var hashedPassword = Encryption.Hash(entity.Password, _settings.EncryptionKey);
            entity.Password = hashedPassword;

            _repository.Add(entity);
            await context.SaveChangesAsync();

            return Result.Success<object>(entity.Id);
        }
        catch(Exception ex)
        {
            return Result.Error<object>("Error creating user", ex.Message);
        }
    }

    public async Task<BaseResult<LoginResponse>> LoginAsync(string email, string password)
    {
        try
        {
            var user = await _repository
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return Result.Unauthorized<LoginResponse>();

            if (!Encryption.Compare(password, user.Password, _settings.EncryptionKey))
                return Result.Unauthorized<LoginResponse>();

            var accessToken = jwtService.CreateAccessToken(user);

            return Result.Success(new LoginResponse { AccessToken = accessToken, Expires = 0 });
        }
        catch(Exception ex)
        {
            return Result.Error<LoginResponse>("Error authenticating user", ex.Message);
        }
    }
}
