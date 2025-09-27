using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Entities.Semestres;
using PeiFeira.Domain.Entities.Turmas;
using PeiFeira.Domain.Entities.Usuarios;
using PeiFeira.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Projetos;

[Table("Projeto")]
public class Projeto : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public Guid SemestreId { get; set; }
    public Guid TurmaId { get; set; }
    public Guid PerfilProfessorOrientadorId { get; set; } 

    public string Titulo { get; set; } = string.Empty;
    public string Tema { get; set; } = string.Empty;
    public string DesafioProposto { get; set; } = string.Empty;
    public StatusProjeto Status { get; set; } = StatusProjeto.EmAndamento;

    // Dados da empresa/local (opcionais)
    public string? NomeEmpresa { get; set; }
    public string? EnderecoCompleto { get; set; }
    public string? Cidade { get; set; }
    public string? RedeSocial { get; set; }
    public string? Contato { get; set; }

    // Dados do responsável na empresa (opcionais)
    public string? NomeResponsavel { get; set; }
    public string? CargoResponsavel { get; set; }
    public string? TelefoneResponsavel { get; set; }
    public string? EmailResponsavel { get; set; }
    public string? RedesSociaisResponsavel { get; set; }

    public virtual Semestre Semestre { get; set; } = null!;
    public virtual Turma Turma { get; set; } = null!;
    public virtual PerfilProfessor ProfessorOrientador { get; set; } = null!;
    public virtual ICollection<Equipe> Equipes { get; set; } = new List<Equipe>(); // 1:N
    public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
}