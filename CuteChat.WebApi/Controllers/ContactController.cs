using CuteChat.Models;
using CuteChat.Models.Contact;
using CuteChat.Services.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuteChat.WebApi.Controllers;

[ApiController]
[Route("api/v1/contact")]
[Authorize]
public class ContactController(ContactManager contactManager) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContactResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var userId = User.GetUserId();
            if (userId is null) return Unauthorized();

            var result = await contactManager.GetAllAsync(userId.Value);
            return result.ToActionResult();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.StackTrace);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatedEntityResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(CreateContactRequest request)
    {
        try
        {
            var userId = User.GetUserId();
            if (userId is null) return Unauthorized();

            var result = await contactManager.CreateAsync(userId.Value, request);
            if (!result.IsSuccess)
                return result.ToActionResult();

            return Ok(new CreatedEntityResponse<int>(result.Data));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.StackTrace);
        }
    }
}
