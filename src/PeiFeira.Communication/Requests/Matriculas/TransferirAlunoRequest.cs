namespace PeiFeira.Communication.Requests.Matriculas;

public class TransferirAlunoRequest
{
    public Guid PerfilAlunoId { get; set; }
    public Guid NovaTurmaId { get; set; }
}
