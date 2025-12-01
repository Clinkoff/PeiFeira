using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Turmas;

public class TurmaResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public Guid SemestreId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string? Curso { get; set; }
    public int? Periodo { get; set; }
    public string? Turno { get; set; }

    public SemestreSimplificadoResponse? Semestre { get; set; }
}

public class SemestreSimplificadoResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int Periodo { get; set; }
}