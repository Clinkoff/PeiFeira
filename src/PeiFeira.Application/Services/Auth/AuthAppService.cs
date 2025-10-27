using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.Auth;
using PeiFeira.Communication.Responses.Auth;

namespace PeiFeira.Application.Services.Auth;

public class AuthAppService
{
    private readonly IAuthManager _authManager;
    private readonly ILogger<AuthAppService> _logger;

    public AuthAppService(
        IAuthManager authManager,
        ILogger<AuthAppService> logger)
    {
        _authManager = authManager;
        _logger = logger;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        _logger.LogInformation("Tentativa de login: {Matricula}", request.Matricula);

        var response = await _authManager.LoginAsync(request);

        _logger.LogInformation("Login bem-sucedido: {Matricula}, Role: {Role}",
            request.Matricula, response.Usuario.Tipo);

        return response;
    }

    public async Task<UserInfo> GetUserInfoAsync(Guid userId)
    {
        _logger.LogInformation("Buscando informações do usuário: {UserId}", userId);
        return await _authManager.GetUserInfoAsync(userId);
    }
}