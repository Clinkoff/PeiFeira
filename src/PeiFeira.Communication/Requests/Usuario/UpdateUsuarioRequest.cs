
using PeiFeira.Communication.Enums;

namespace PeiFeira.Communication.Requests.Usuario;

public class UpdateUsuarioRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRoleDto Role { get; set; }
}
