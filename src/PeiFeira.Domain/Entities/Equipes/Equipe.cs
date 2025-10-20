using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Entities.Usuarios;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Equipes;

[Table("Equipe")]
public class Equipe : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
    public Guid LiderPerfilAlunoId { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string? UrlQrCode { get; set; }
    public string? CodigoConvite { get; set; }


    public virtual PerfilAluno Lider { get; set; } = null!;
    public virtual Projeto? Projeto { get; set; }
    public virtual ICollection<MembroEquipe> Membros { get; set; } = new List<MembroEquipe>();
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
}