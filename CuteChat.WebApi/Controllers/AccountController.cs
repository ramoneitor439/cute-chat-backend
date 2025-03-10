using CuteChat.Models.AppUser;
using CuteChat.Models.AppUser.Mappers;
using CuteChat.Services.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuteChat.WebApi.Controllers
{
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController(AppUserManager manager) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync(RegisterUserRequest request)
        {
            var result = await manager.CreateAsync(request.ToBaseEntity());
            return result.ToActionResult();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var result = await manager.LoginAsync(request.Email, request.Password);
            return result.ToActionResult();
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(UserInfoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfoAsync()
        {
            var userId = User.GetUserId();
            if (userId is null) return Unauthorized();

            try
            {
                var userResult = await manager.GetByIdAsync(userId);
                if(!userResult.IsSuccess)
                    return userResult.ToActionResult();

                return Ok(userResult.Data?.ToUserInfo());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
