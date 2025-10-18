using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Entities.Semestres;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.DisciplinasPI;

[Table("DisciplinaPI")]
public class DisciplinaPI : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
    public Guid SemestreId { get; set; }
    public Guid PerfilProfessorId { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string TemaGeral { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Objetivos { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public StatusProjetoIntegrador Status { get; set; } = StatusProjetoIntegrador.Ativo;

    // Navegação
    public virtual Semestre Semestre { get; set; } = null!;
    public virtual PerfilProfessor Professor { get; set; } = null!;
    public virtual ICollection<DisciplinaPITurma> DisciplinaPITurmas { get; set; } = new List<DisciplinaPITurma>();
    public virtual ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();
}
