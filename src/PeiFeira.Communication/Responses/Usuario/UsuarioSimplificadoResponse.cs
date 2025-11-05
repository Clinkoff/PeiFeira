namespace PeiFeira.Communication.Responses.Usuarios;

public class UsuarioSimplificadoResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Curso { get; set; }
    public string? Turno { get; set; }
}
