using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.Turma;
using PeiFeira.Communication.Responses.Turmas;

namespace PeiFeira.Application.Services.Turmas;

public class TurmaAppService
{
    private readonly ITurmaManager _turmaManager;
    private readonly ILogger<TurmaAppService> _logger;

    public TurmaAppService(
        ITurmaManager turmaManager,
        ILogger<TurmaAppService> logger)
    {
        _turmaManager = turmaManager;
        _logger = logger;
    }

    public async Task<TurmaResponse> CriarAsync(CreateTurmaRequest request)
    {
        _logger.LogInformation("Iniciando criação de turma: {Nome}", request.Nome);
        var response = await _turmaManager.CreateAsync(request);
        _logger.LogInformation("Turma criada com sucesso. ID: {Id}", response.Id);
        return response;
    }

    public async Task<TurmaResponse> AtualizarAsync(Guid id, UpdateTurmaRequest request)
    {
        _logger.LogInformation("Iniciando atualização de turma. ID: {Id}", id);
        var response = await _turmaManager.UpdateAsync(id, request);
        _logger.LogInformation("Turma atualizada com sucesso. ID: {Id}", id);
        return response;
    }

    public async Task<bool> ExcluirAsync(Guid id)
    {
        _logger.LogInformation("Iniciando exclusão de turma. ID: {Id}", id);
        var result = await _turmaManager.DeleteAsync(id);
        if (result)
            _logger.LogInformation("Turma excluída com sucesso. ID: {Id}", id);
        else
            _logger.LogWarning("Falha ao excluir turma. ID: {Id}", id);
        return result;
    }

    public async Task<TurmaResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando turma por ID: {Id}", id);
        return await _turmaManager.GetByIdAsync(id);
    }

    public async Task<IEnumerable<TurmaResponse>> ListarTodosAsync()
    {
        _logger.LogInformation("Listando todas as turmas");
        return await _turmaManager.GetAllAsync();
    }

    public async Task<IEnumerable<TurmaResponse>> ListarAtivosAsync()
    {
        _logger.LogInformation("Listando turmas ativas");
        return await _turmaManager.GetActiveAsync();
    }

    public async Task<IEnumerable<TurmaResponse>> ListarPorSemestreIdAsync(Guid semestreId)
    {
        _logger.LogInformation("Listando turmas do semestre: {SemestreId}", semestreId);
        return await _turmaManager.GetBySemestreIdAsync(semestreId);
    }

    public async Task<IEnumerable<TurmaResponse>> ListarPorCursoAsync(string curso)
    {
        _logger.LogInformation("Listando turmas do curso: {Curso}", curso);
        return await _turmaManager.GetByCursoAsync(curso);
    }

    public async Task<TurmaResponse?> BuscarPorCodigoAsync(string codigo)
    {
        _logger.LogInformation("Buscando turma por código: {Codigo}", codigo);
        return await _turmaManager.GetByCodigoAsync(codigo);
    }
}
