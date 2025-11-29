namespace PeiFeira.Communication.Responses.Dashboard;

public class ProjetosPorMesResponse
{
    public string Mes { get; set; } = string.Empty;
    public int Criados { get; set; }
    public int Concluidos { get; set; }
}