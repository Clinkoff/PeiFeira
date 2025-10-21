using PeiFeira.Communication.Enums;
using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Projetos;

public class ProjetoDetailResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public Guid DisciplinaPIId { get; set; }
    public Guid EquipeId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string DesafioProposto { get; set; } = string.Empty;
    public StatusProjetoDto Status { get; set; }

    // Dados da empresa/local (opcionais)
    public string? NomeEmpresa { get; set; }
    public string? EnderecoCompleto { get; set; }
    public string? Cidade { get; set; }
    public string? RedeSocial { get; set; }
    public string? Contato { get; set; }

    // Dados do responsável na empresa (opcionais)
    public string? NomeResponsavel { get; set; }
    public string? CargoResponsavel { get; set; }
    public string? TelefoneResponsavel { get; set; }
    public string? EmailResponsavel { get; set; }
    public string? RedesSociaisResponsavel { get; set; }

    // Dados detalhados
    public EquipeProjetoInfo? Equipe { get; set; }
    public DisciplinaPIProjetoInfo? DisciplinaPI { get; set; }
    public List<AvaliacaoResumo> Avaliacoes { get; set; } = new();
}

public class EquipeProjetoInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public LiderProjetoInfo? Lider { get; set; }
    public List<MembroProjetoInfo> Membros { get; set; } = new();
}

public class LiderProjetoInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class MembroProjetoInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class DisciplinaPIProjetoInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Professor { get; set; }
}

public class AvaliacaoResumo
{
    public Guid Id { get; set; }
    public decimal Nota { get; set; }
    public string Avaliador { get; set; } = string.Empty;
    public DateTime DataAvaliacao { get; set; }
}
