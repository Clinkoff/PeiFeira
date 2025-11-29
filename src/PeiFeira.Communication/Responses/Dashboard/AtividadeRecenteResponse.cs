namespace PeiFeira.Communication.Responses.Dashboard;

public class AtividadeRecenteResponse
{
    public Guid Id { get; set; }
    public string Tipo { get; set; } = string.Empty; // "projeto", "equipe", "disciplina", "usuario"
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string? Icone { get; set; }
}