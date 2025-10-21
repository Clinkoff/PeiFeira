using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Avaliacoes;

public class AvaliacaoResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
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

    // Dados relacionados
    public string? NomeEquipe { get; set; }
    public string? NomeAvaliador { get; set; }
}
