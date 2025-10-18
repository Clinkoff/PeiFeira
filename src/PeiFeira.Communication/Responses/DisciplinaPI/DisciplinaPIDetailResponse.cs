using PeiFeira.Communication.Enums;
using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.DisciplinaPI;

public class DisciplinaPIDetailResponse : BaseResponse
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

    // Navegação detalhada
    public SemestreInfo? Semestre { get; set; }
    public ProfessorInfo? Professor { get; set; }
    public List<TurmaInfo> Turmas { get; set; } = new();
    public List<ProjetoInfo> Projetos { get; set; } = new();
}

public class SemestreInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int Periodo { get; set; }
}

public class ProfessorInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Departamento { get; set; }
}

public class TurmaInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Curso { get; set; }
}

public class ProjetoInfo
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? NomeEquipe { get; set; }
}
