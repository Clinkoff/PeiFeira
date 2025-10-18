using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Turmas;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.DisciplinasPI;

[Table("DisciplinaPITurma")]
public class DisciplinaPITurma : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public Guid DisciplinaPIId { get; set; }
    public Guid TurmaId { get; set; }

    // Navegação
    public virtual DisciplinaPI DisciplinaPI { get; set; } = null!;
    public virtual Turma Turma { get; set; } = null!;
}
