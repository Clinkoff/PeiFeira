namespace PeiFeira.Communication.Requests.Auth;

public class LoginRequest
{
    public string Matricula { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
