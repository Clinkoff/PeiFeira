namespace PeiFeira.Communication.Requests.Turma;

public class UpdateTurmaRequest
{
    public string Nome { get; set; } = string.Empty;
    public string? Curso { get; set; }
    public int? Periodo { get; set; }
    public string? Turno { get; set; }
}
