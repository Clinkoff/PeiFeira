namespace PeiFeira.Communication.Responses.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public int ExpiresIn { get; set; } // Segundos até expirar
    public UserInfo Usuario { get; set; } = null!;
}
