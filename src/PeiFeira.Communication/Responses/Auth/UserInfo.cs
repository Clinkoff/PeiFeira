using PeiFeira.Communication.Responses.Usuario;

namespace PeiFeira.Communication.Responses.Auth;

public class UserInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // Mudei de 'Tipo' para 'Role' pra bater com seu front

    public PerfilProfessorResponse? PerfilProfessor { get; set; }
    public PerfilAlunoResponse? PerfilAluno { get; set; }
}