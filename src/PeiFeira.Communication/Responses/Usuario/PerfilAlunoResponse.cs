using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Usuario;

public class PerfilAlunoResponse
{
    public Guid Id { get; set; }
    public string? Curso { get; set; }
    public string? Turno { get; set; }
}
