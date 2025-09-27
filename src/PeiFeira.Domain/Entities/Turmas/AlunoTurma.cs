using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Domain.Entities.Turmas;

public class AlunoTurma : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
    public Guid TurmaId { get; set; }
    public Guid PerfilAlunoId { get; set; }
    public DateTime DataMatricula { get; set; }
    public bool IsAtual { get; set; } = true; // Se é a matrícula atual ou histórico

    public virtual Turma Turma { get; set; } = null!;
    public virtual PerfilAluno PerfilAluno { get; set; } = null!;
}
