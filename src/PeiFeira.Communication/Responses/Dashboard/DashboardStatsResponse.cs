namespace PeiFeira.Communication.Responses.Dashboard;

public class DashboardStatsResponse
{
    public int TotalSemestres { get; set; }
    public int TotalUsuarios { get; set; }
    public int TotalAlunos { get; set; }
    public int TotalProfessores { get; set; }
    public int TotalTurmas { get; set; }
    public int TotalDisciplinasPI { get; set; }
    public int TotalEquipes { get; set; }
    public int TotalProjetos { get; set; }
    public int DisciplinasPIAtivas { get; set; }
    public int ProjetosEmAndamento { get; set; }
    public int EquipesAtivas { get; set; }
}