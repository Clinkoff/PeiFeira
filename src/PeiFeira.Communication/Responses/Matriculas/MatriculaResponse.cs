using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Matriculas;

public class MatriculaResponse : BaseResponse
{
    public Guid Id { get; set; }
    public Guid PerfilAlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public bool IsActive { get; set; }
    public DateTime DataMatricula { get; set; }
    public bool IsAtual { get; set; }
    public string? NomeTurma { get; set; }
    public string? CodigoTurma { get; set; }
    public string? NomeAluno { get; set; }
    public string? MatriculaAluno { get; set; }
}
