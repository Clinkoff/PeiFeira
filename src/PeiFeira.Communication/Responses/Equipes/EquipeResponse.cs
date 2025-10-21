using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Equipes;

public class EquipeResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public Guid LiderPerfilAlunoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CodigoConvite { get; set; }
    public string? UrlQrCode { get; set; }
    public string? NomeLider { get; set; }
    public int QuantidadeMembros { get; set; }
    public bool TemProjeto { get; set; }
}
