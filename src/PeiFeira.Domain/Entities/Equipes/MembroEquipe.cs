using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Usuarios;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Equipes;

[Table("MembroEquipe")]
public class MembroEquipe : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public Guid EquipeId { get; set; }
    public Guid PerfilAlunoId { get; set; }

    public TeamMemberRole Cargo { get; set; }
    public string? Funcao { get; set; }
    public DateTime IngressouEm { get; set; } = DateTime.UtcNow;
    public DateTime? SaiuEm { get; set; }

    public virtual Equipe Equipe { get; set; } = null!;
    public virtual PerfilAluno PerfilAluno { get; set; } = null!;
}