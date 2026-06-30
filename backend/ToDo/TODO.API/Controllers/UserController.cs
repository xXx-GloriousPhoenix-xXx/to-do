using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TODO.BLL.User;
using TODO.Interfaces.User;
using TODO.Interfaces.User.DTO;

namespace TODO.API.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("me")]
    public async Task<ActionResult<GetUserDTO>> GetMe()
    {
        var userId = GetUserId();
        var result = await _userService.GetByIdAsync(userId);
        return Ok(result);
    }

    [HttpPatch("me")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDTO dto)
    {
        var userId = GetUserId();
        await _userService.UpdateProfileAsync(userId, dto);
        return NoContent();
    }

    [HttpPost("me/change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
    {
        var userId = GetUserId();
        await _userService.ChangePasswordAsync(userId, dto);
        return NoContent();
    }

    [HttpDelete("me")]
    public async Task<IActionResult> DeleteAccount()
    {
        var userId = GetUserId();
        await _userService.DeleteAsync(userId);
        return NoContent();
    }

    private Guid GetUserId()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(idClaim!);
    }
}
