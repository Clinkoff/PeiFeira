using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.MembroEquipe;

public class MembroEquipeResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public Guid EquipeId { get; set; }
    public Guid PerfilAlunoId { get; set; }
    public DateTime DataEntrada { get; set; }
    public string? NomeEquipe { get; set; }
    public string? NomeMembro { get; set; }
    public string? EmailMembro { get; set; }
}
