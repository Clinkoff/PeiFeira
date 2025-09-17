using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Enums;

namespace PeiFeira.Domain.Entities.Equipes;

public class ConviteEquipe : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public Guid EquipeId { get; set; }
    public Guid ConvidadoPorId { get; set; }  
    public Guid ConvidadoId { get; set; }     
    public string? Mensagem { get; set; }

    public StatusConvite Status { get; set; } = StatusConvite.Pendente;
    public string? MotivoResposta { get; set; }
    public DateTime? RespondidoEm { get; set; }

    public virtual Equipe Equipe { get; set; } = null!;
    public virtual Usuario ConvidadoPor { get; set; } = null!;
    public virtual Usuario Convidado { get; set; } = null!;
}
