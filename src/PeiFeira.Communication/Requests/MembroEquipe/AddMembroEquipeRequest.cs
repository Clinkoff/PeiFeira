namespace PeiFeira.Communication.Requests.MembroEquipe;

public class AddMembroEquipeRequest
{
    public Guid EquipeId { get; set; }
    public Guid PerfilAlunoId { get; set; }
}
