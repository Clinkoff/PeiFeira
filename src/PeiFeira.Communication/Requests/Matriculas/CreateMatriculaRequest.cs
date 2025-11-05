namespace PeiFeira.Communication.Requests.Matriculas;

public class CreateMatriculaRequest
{
    public Guid TurmaId { get; set; }
    public Guid UsuarioId { get; set; }
}
