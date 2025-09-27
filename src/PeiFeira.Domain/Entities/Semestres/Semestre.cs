using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Entities.Turmas;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Semestres;

[Table("Semestre")]
public class Semestre : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int Periodo { get; set; } // 1 para primeiro semestre, 2 para segundo semestre
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }

    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
    public virtual ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();
}
