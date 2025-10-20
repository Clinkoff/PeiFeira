namespace PeiFeira.Communication.Requests.Semestres;

public class UpdateSemestreRequest
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}
