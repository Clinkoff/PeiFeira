using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.Equipes;

namespace PeiFeira.Domain.Entities.Usuarios;

public class Usuario : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public string Matricula { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    // Navigation Properties
    public virtual ICollection<MembroEquipe> MembroEquipes { get; set; } = new List<MembroEquipe>();
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
}