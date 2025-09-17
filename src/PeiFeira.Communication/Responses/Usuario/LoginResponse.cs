namespace PeiFeira.Communication.Responses.Usuario;

public class LoginResponse
{
    public UsuarioResponse Usuario { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
