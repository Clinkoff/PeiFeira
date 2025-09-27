using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.Equipes;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Usuarios;

[Table("Usuario")]
public class Usuario : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public string Matricula { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public virtual PerfilAluno? PerfilAluno { get; set; }
    public virtual PerfilProfessor? PerfilProfessor { get; set; }
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
}