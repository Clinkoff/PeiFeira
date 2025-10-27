using PeiFeira.Communication.Requests.Auth;
using PeiFeira.Communication.Responses.Auth;

namespace PeiFeira.Application.Services.Auth;

public interface IAuthManager
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<UserInfo> GetUserInfoAsync(Guid userId);
}