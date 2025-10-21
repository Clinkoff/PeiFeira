using PeiFeira.Communication.Enums;
using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.ConviteEquipe;

public class ConviteEquipeResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public Guid EquipeId { get; set; }
    public Guid ConvidadoPorId { get; set; }
    public Guid ConvidadoId { get; set; }
    public StatusConvite Status { get; set; }
    public DateTime? DataResposta { get; set; }
    public string? Mensagem { get; set; }
    public string? MotivoResposta { get; set; }
    public string? NomeEquipe { get; set; }
    public string? NomeConvidadoPor { get; set; }
    public string? NomeConvidado { get; set; }
}
