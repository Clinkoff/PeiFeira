using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Auth;
using PeiFeira.Communication.Requests.Auth;
using PeiFeira.Communication.Responses.Auth;
using System.Security.Claims;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthAppService _authAppService;

    public AuthController(AuthAppService authAppService)
    {
        _authAppService = authAppService;
    }

 
    [HttpPost("login")]
    [AllowAnonymous] 
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var response = await _authAppService.LoginAsync(request);
        return Ok(response);
    }

    [HttpGet("me")]
    [Authorize] 
    public async Task<ActionResult<UserInfo>> GetMe()
    {
        // Pegar ID do usuário do token JWT
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var userInfo = await _authAppService.GetUserInfoAsync(userId);
        return Ok(userInfo);
    }
}