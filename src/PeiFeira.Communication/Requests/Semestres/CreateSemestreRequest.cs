namespace PeiFeira.Communication.Requests.Semestres;

public class CreateSemestreRequest
{
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int Periodo { get; set; } // 1 ou 2
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}
