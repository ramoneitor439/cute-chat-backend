using CuteChat.Models.AppUser.Message;
using CuteChat.Services.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuteChat.WebApi.Controllers;

[ApiController]
[Route("api/v1/message")]
[Authorize]
public class MessageController(MessageManager manager) : ControllerBase
{
    [HttpGet("{contactUserId}")]
    [ProducesResponseType(typeof(IEnumerable<ListMessageResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserAsync(int contactUserId)
    {
        var userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        try
        {
            var result = await manager.GetAllByUserAsync(userId.Value, contactUserId);
            return result.ToActionResult();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.StackTrace);
        }
    }
}
