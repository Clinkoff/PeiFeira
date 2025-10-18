using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Equipes;

[Table("ConviteEquipe")]
public class ConviteEquipe : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public Guid EquipeId { get; set; }
    public Guid ConvidadoPorPerfilAlunoId { get; set; }
    public Guid ConvidadoPerfilAlunoId { get; set; }
    public string? Mensagem { get; set; }

    public StatusConvite Status { get; set; } = StatusConvite.Pendente;
    public string? MotivoResposta { get; set; }
    public DateTime? RespondidoEm { get; set; }

    public virtual Equipe Equipe { get; set; } = null!;
    public virtual PerfilAluno ConvidadoPor { get; set; } = null!;
    public virtual PerfilAluno Convidado { get; set; } = null!;
}
