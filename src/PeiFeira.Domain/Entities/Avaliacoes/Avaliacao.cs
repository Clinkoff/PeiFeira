using PeiFeira.Domain.Bases;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Entities.Usuarios;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeiFeira.Domain.Entities.Avaliacoes;

[Table("Avaliacao")]
public class Avaliacao : Auditable, IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;

    public Guid EquipeId { get; set; }
    public Guid AvaliadorId { get; set; }

    // Definição do problema (0-5)
    public int RelevanciaProblema { get; set; }
    public int FundamentacaoProblema { get; set; }

    // Defesa da solução (0-5)
    public int FocoSolucao { get; set; }
    public int ViabilidadeSolucao { get; set; }

    // Apresentação (0-5)
    public int ClarezaApresentacao { get; set; }
    public int DominioAssunto { get; set; }
    public int TransmissaoInformacoes { get; set; }
    public int PadronizacaoApresentacao { get; set; }
    public int LinguagemTempo { get; set; }
    public int QualidadeRespostas { get; set; }

    public decimal? PontuacaoTotal { get; set; }
    public decimal? NotaFinal { get; set; }

    public string? Comentarios { get; set; }

    public virtual Equipe Equipe { get; set; } = null!;
    public virtual PerfilProfessor Avaliador { get; set; } = null!;

   
}