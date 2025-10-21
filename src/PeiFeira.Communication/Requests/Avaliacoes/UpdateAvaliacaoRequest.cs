namespace PeiFeira.Communication.Requests.Avaliacoes;

public class UpdateAvaliacaoRequest
{
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

    public string? Comentarios { get; set; }
}
