using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Equipes;

public class EquipeDetailResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public Guid LiderPerfilAlunoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CodigoConvite { get; set; }
    public string? UrlQrCode { get; set; }
    public int QuantidadeMembros { get; set; }
    public bool TemProjeto { get; set; }

    public LiderInfo? Lider { get; set; }
    public List<MembroInfo> Membros { get; set; } = new();
    public ProjetoInfo? Projeto { get; set; }
}

public class LiderInfo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class MembroInfo
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataEntrada { get; set; }
}

public class ProjetoInfo
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
