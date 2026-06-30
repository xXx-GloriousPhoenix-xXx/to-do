using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TODO.Interfaces.Auth;
using TODO.Interfaces.Auth.DTO;
using TODO.Interfaces.Common;

namespace TODO.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("sign-up")]
    public async Task<ActionResult<TokenResponse>> SignUp(SignUpDTO dto)
    {
        var result = await _authService.SignUpAsync(dto);
        return Ok(result);
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<TokenResponse>> SignIn(SignInDTO dto)
    {
        var result = await _authService.SignInAsync(dto);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenResponse>> Refresh([FromBody] string refreshToken)
    {
        var result = await _authService.RefreshAsync(refreshToken);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("sign-out")]
    public async Task<IActionResult> SignOut([FromBody] string refreshToken)
    {
        await _authService.SignOutAsync(refreshToken);
        return NoContent();
    }
}
