using PeiFeira.Communication.Enums;

namespace PeiFeira.Communication.Requests.Usuario;

public class CreateUsuarioRequest
{
   public string Matricula { get; set; } = string.Empty;
   public string Nome { get; set; } = string.Empty;
   public string Email { get; set; } = string.Empty;
   public string Senha { get; set; } = string.Empty;
   public UserRoleDto Role { get; set; }

   public PerfilAlunoRequest? PerfilAluno;
   public PerfilProfessorRequest? PerfilProfessor;
}
