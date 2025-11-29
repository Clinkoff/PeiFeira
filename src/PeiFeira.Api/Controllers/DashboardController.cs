// PeiFeira.Api/Controllers/DashboardController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeiFeira.Application.Services.Dashboard;
using PeiFeira.Communication.Responses.Dashboard;

namespace PeiFeira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardManager _dashboardManager;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(
        IDashboardManager dashboardManager,
        ILogger<DashboardController> logger)
    {
        _dashboardManager = dashboardManager;
        _logger = logger;
    }

    /// <summary>
    /// Retorna estatísticas gerais do sistema
    /// </summary>
    [HttpGet("stats")]
    [ProducesResponseType(typeof(DashboardStatsResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<DashboardStatsResponse>> GetStats()
    {
        _logger.LogInformation("Buscando estatísticas do dashboard");
        var stats = await _dashboardManager.GetStatsAsync();
        return Ok(stats);
    }

    /// <summary>
    /// Retorna quantidade de projetos agrupados por status
    /// </summary>
    [HttpGet("projetos-por-status")]
    [ProducesResponseType(typeof(IEnumerable<ProjetosPorStatusResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjetosPorStatusResponse>>> GetProjetosPorStatus()
    {
        _logger.LogInformation("Buscando projetos agrupados por status");
        var projetos = await _dashboardManager.GetProjetosPorStatusAsync();
        return Ok(projetos);
    }

    /// <summary>
    /// Retorna quantidade de disciplinas PI agrupadas por semestre
    /// </summary>
    [HttpGet("disciplinas-por-semestre")]
    [ProducesResponseType(typeof(IEnumerable<DisciplinasPorSemestreResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DisciplinasPorSemestreResponse>>> GetDisciplinasPorSemestre()
    {
        _logger.LogInformation("Buscando disciplinas agrupadas por semestre");
        var disciplinas = await _dashboardManager.GetDisciplinasPorSemestreAsync();
        return Ok(disciplinas);
    }

    /// <summary>
    /// Retorna evolução de projetos nos últimos 6 meses
    /// </summary>
    [HttpGet("projetos-por-mes")]
    [ProducesResponseType(typeof(IEnumerable<ProjetosPorMesResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjetosPorMesResponse>>> GetProjetosPorMes()
    {
        _logger.LogInformation("Buscando evolução de projetos por mês");
        var projetos = await _dashboardManager.GetProjetosPorMesAsync();
        return Ok(projetos);
    }

    /// <summary>
    /// Retorna top 10 turmas com mais alunos
    /// </summary>
    [HttpGet("alunos-por-turma")]
    [ProducesResponseType(typeof(IEnumerable<AlunosPorTurmaResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AlunosPorTurmaResponse>>> GetAlunosPorTurma()
    {
        _logger.LogInformation("Buscando alunos agrupados por turma");
        var alunos = await _dashboardManager.GetAlunosPorTurmaAsync();
        return Ok(alunos);
    }

    /// <summary>
    /// Retorna atividades recentes do sistema
    /// </summary>
    [HttpGet("atividades-recentes")]
    [ProducesResponseType(typeof(IEnumerable<AtividadeRecenteResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AtividadeRecenteResponse>>> GetAtividadesRecentes()
    {
        _logger.LogInformation("Buscando atividades recentes");
        var atividades = await _dashboardManager.GetAtividadesRecentesAsync();
        return Ok(atividades);
    }
}