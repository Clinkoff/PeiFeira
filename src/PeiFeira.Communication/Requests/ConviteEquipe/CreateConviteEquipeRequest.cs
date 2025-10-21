namespace PeiFeira.Communication.Requests.ConviteEquipe;

public class CreateConviteEquipeRequest
{
    public Guid EquipeId { get; set; }
    public Guid ConvidadoPorId { get; set; }
    public Guid ConvidadoId { get; set; }
    public string? Mensagem { get; set; }
}
