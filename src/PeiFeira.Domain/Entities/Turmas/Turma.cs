using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Entities.Semestres;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Turmas;

[Table("Turma")]
public class Turma : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
    public Guid SemestreId { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty; // Código único para a turma
    public string? Curso { get; set; } 
    public int? Periodo { get; set; }
    public string? Turno { get; set; } // Manhã, Tarde, Noite, Integral

    public virtual Semestre Semestre { get; set; } = null!;
    public virtual ICollection<AlunoTurma> AlunosTurma { get; set; } = new List<AlunoTurma>();
}
