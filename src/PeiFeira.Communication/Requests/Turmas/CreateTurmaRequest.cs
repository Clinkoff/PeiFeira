namespace PeiFeira.Communication.Requests.Turma;

public class CreateTurmaRequest
{
    public Guid SemestreId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string? Curso { get; set; }
    public int? Periodo { get; set; }
    public string? Turno { get; set; }
}
