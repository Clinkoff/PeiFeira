using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Entities.DisciplinasPI;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Projetos;

[Table("Projeto")]
public class Projeto : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
    public Guid DisciplinaPIId { get; set; }
    public Guid EquipeId { get; set; }
    public string Titulo { get; set; } = string.Empty;
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

    public virtual DisciplinaPI DisciplinaPI { get; set; } = null!;
    public virtual Equipe Equipe { get; set; } = null!;
}