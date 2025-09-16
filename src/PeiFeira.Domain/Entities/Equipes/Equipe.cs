using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Entities.Usuarios;

namespace PeiFeira.Domain.Entities.Equipes;

public class Equipe : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public string Nome { get; set; } = string.Empty;
    public Guid LiderId { get; set; }
    public string? UrlQrCode { get; set; }
    public string? CodigoConvite { get; set; }

    // Navigation Properties
    public virtual Usuario Lider { get; set; } = null!;
    public virtual ICollection<MembroEquipe> Membros { get; set; } = new List<MembroEquipe>();
    public virtual Projeto? Projeto { get; set; }
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
}