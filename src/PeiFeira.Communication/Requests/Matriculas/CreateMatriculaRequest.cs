namespace PeiFeira.Communication.Requests.Matriculas;

public class CreateMatriculaRequest
{
    public Guid PerfilAlunoId { get; set; }
    public Guid TurmaId { get; set; }
}
