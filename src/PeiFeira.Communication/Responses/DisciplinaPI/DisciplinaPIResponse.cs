using PeiFeira.Communication.Enums;
using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.DisciplinaPI;

public class DisciplinaPIResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public Guid SemestreId { get; set; }
    public Guid PerfilProfessorId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string TemaGeral { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Objetivos { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public StatusProjetoIntegradorDto Status { get; set; }

    // Dados resumidos
    public string? NomeSemestre { get; set; }
    public string? NomeProfessor { get; set; }
    public int QuantidadeTurmas { get; set; }
    public int QuantidadeProjetos { get; set; }
}
