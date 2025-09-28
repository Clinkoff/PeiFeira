using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Usuario;

public class UsuarioResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; 
}
