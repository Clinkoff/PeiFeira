using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Entities.Turmas;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Usuarios;

[Table("PerfilAluno")]
public class PerfilAluno : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UsuarioId { get; set; } // Foreign key to Usuario
    public bool IsActive { get; set; } = true;
    public string? Curso { get; set; }
    public int? Periodo { get; set; }
    public virtual Usuario Usuario { get; set; } = null!;
    public virtual ICollection<MembroEquipe> MembroEquipes { get; set; } = new List<MembroEquipe>();
    public virtual ICollection<AlunoTurma> Turmas { get; set; } = new List<AlunoTurma>(); // Histórico de turmas

}
