using PeiFeira.Communication.Enums;

namespace PeiFeira.Communication.Requests.DisciplinaPI;

public class UpdateDisciplinaPIRequest
{
    public string Nome { get; set; } = string.Empty;
    public string TemaGeral { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Objetivos { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public StatusProjetoIntegradorDto Status { get; set; }
    public List<Guid> TurmaIds { get; set; } = new();
}
