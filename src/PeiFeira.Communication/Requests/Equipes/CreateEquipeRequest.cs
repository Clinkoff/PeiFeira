namespace PeiFeira.Communication.Requests.Equipes;

public class CreateEquipeRequest
{
    public Guid LiderPerfilAlunoId { get; set; }
    public string Nome { get; set; } = string.Empty;
}
