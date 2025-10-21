namespace PeiFeira.Communication.Requests.Projetos;

public class CreateProjetoRequest
{
    public Guid DisciplinaPIId { get; set; }
    public Guid EquipeId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string DesafioProposto { get; set; } = string.Empty;

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
}
