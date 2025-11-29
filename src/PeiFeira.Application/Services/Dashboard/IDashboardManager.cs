using PeiFeira.Communication.Responses.Dashboard;

namespace PeiFeira.Application.Services.Dashboard;

public interface IDashboardManager
{
    Task<DashboardStatsResponse> GetStatsAsync();
    Task<IEnumerable<ProjetosPorStatusResponse>> GetProjetosPorStatusAsync();
    Task<IEnumerable<DisciplinasPorSemestreResponse>> GetDisciplinasPorSemestreAsync();
    Task<IEnumerable<ProjetosPorMesResponse>> GetProjetosPorMesAsync();
    Task<IEnumerable<AlunosPorTurmaResponse>> GetAlunosPorTurmaAsync();
    Task<IEnumerable<AtividadeRecenteResponse>> GetAtividadesRecentesAsync();
}