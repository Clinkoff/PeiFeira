using PeiFeira.Communication.Responses.Base;

namespace PeiFeira.Communication.Responses.Semestres;

public class SemestreResponse : BaseResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int Periodo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}

