namespace PeiFeira.Communication.Responses.Auth;

public class UserInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; 
    public Guid? PerfilId { get; set; } // PerfilAlunoId ou PerfilProfessorId ou outros Perfis que add
}