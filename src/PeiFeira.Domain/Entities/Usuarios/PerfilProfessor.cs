using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Projetos;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Usuarios;

[Table("PerfilProfessor")]
public class PerfilProfessor : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
    public Guid UsuarioId { get; set; } // Foreign key to Usuario

    public string? Departamento { get; set; } 
    public string? AreaEspecializacao { get; set; } 
    public string? Titulacao { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
    public virtual ICollection<Projeto> ProjetosOrientados { get; set; } = new List<Projeto>();
}
