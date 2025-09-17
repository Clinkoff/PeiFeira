using System.ComponentModel.DataAnnotations;

namespace PeiFeira.Communication.Requests.Usuario;

public class UpdateUsuarioRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Role { get; set; }
    }
