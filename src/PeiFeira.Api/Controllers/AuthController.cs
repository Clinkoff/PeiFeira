using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Auth;
using PeiFeira.Communication.Requests.Auth;
using PeiFeira.Communication.Responses.Auth;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthManager authManager, ILogger<AuthController> logger)
    {
        _authManager = authManager;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        _logger.LogInformation("Tentativa de login");
        var response = await _authManager.LoginAsync(request);
        _logger.LogInformation("Login bem-sucedido - UserId: {UserId}", response.Usuario.Id);
        return Ok(response);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserInfo>> GetMe()
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var userInfo = await _authManager.GetUserInfoAsync(userId);
        return Ok(userInfo);
    }

    [HttpPost("logout")]
    [Authorize]
    public ActionResult Logout()
    {
        var userId = User.FindFirst("userId")?.Value;
        var matricula = User.FindFirst("matricula")?.Value;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        _logger.LogInformation(
            "Logout realizado - UserId: {UserId}, Matrícula: {Matricula}, Role: {Role}",
            userId, matricula, role);

        return Ok(new { message = "Logout realizado com sucesso" });
    }
}
