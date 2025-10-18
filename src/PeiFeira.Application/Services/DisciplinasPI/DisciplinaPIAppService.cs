using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.DisciplinaPI;
using PeiFeira.Communication.Responses.DisciplinaPI;

namespace PeiFeira.Application.Services.DisciplinasPI;

public class DisciplinaPIAppService
{
    private readonly IDisciplinaPIManager _disciplinaPIManager;
    private readonly ILogger<DisciplinaPIAppService> _logger;

    public DisciplinaPIAppService(
        IDisciplinaPIManager disciplinaPIManager,
        ILogger<DisciplinaPIAppService> logger)
    {
        _disciplinaPIManager = disciplinaPIManager;
        _logger = logger;
    }

    public async Task<DisciplinaPIResponse> CriarAsync(CreateDisciplinaPIRequest request)
    {
        _logger.LogInformation("Iniciando criação de disciplina PI: {Nome}", request.Nome);

        var response = await _disciplinaPIManager.CreateAsync(request);
        _logger.LogInformation("Disciplina PI criada com sucesso. ID: {Id}", response.Id);
        return response;
    }

    public async Task<DisciplinaPIResponse> AtualizarAsync(Guid id, UpdateDisciplinaPIRequest request)
    {
        _logger.LogInformation("Iniciando atualização de disciplina PI. ID: {Id}", id);

        var response = await _disciplinaPIManager.UpdateAsync(id, request);
        _logger.LogInformation("Disciplina PI atualizada com sucesso. ID: {Id}", id);
        return response;
    }

    public async Task<bool> ExcluirAsync(Guid id)
    {
        _logger.LogInformation("Iniciando exclusão de disciplina PI. ID: {Id}", id);

        var result = await _disciplinaPIManager.DeleteAsync(id);
        if (result)
            _logger.LogInformation("Disciplina PI excluída com sucesso. ID: {Id}", id);
        else
            _logger.LogWarning("Falha ao excluir disciplina PI. ID: {Id}", id);

        return result;
    }

    public async Task<DisciplinaPIResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando disciplina PI por ID: {Id}", id);
        return await _disciplinaPIManager.GetByIdAsync(id);
    }

    public async Task<DisciplinaPIDetailResponse?> BuscarPorIdComDetalhesAsync(Guid id)
    {
        _logger.LogInformation("Buscando disciplina PI com detalhes por ID: {Id}", id);
        return await _disciplinaPIManager.GetByIdWithDetailsAsync(id);
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> ListarTodasAsync()
    {
        _logger.LogInformation("Listando todas as disciplinas PI");
        return await _disciplinaPIManager.GetAllAsync();
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> ListarAtivasAsync()
    {
        _logger.LogInformation("Listando disciplinas PI ativas");
        return await _disciplinaPIManager.GetActiveAsync();
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> ListarPorSemestreAsync(Guid semestreId)
    {
        _logger.LogInformation("Listando disciplinas PI por semestre: {SemestreId}", semestreId);
        return await _disciplinaPIManager.GetBySemestreIdAsync(semestreId);
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> ListarPorProfessorAsync(Guid perfilProfessorId)
    {
        _logger.LogInformation("Listando disciplinas PI por professor: {ProfessorId}", perfilProfessorId);
        return await _disciplinaPIManager.GetByProfessorIdAsync(perfilProfessorId);
    }

    public async Task<IEnumerable<DisciplinaPIResponse>> ListarPorTurmaAsync(Guid turmaId)
    {
        _logger.LogInformation("Listando disciplinas PI por turma: {TurmaId}", turmaId);
        return await _disciplinaPIManager.GetByTurmaIdAsync(turmaId);
    }

    public async Task<bool> AssociarTurmaAsync(Guid disciplinaPIId, Guid turmaId)
    {
        _logger.LogInformation("Associando turma {TurmaId} à disciplina PI {DisciplinaPIId}", turmaId, disciplinaPIId);

        var result = await _disciplinaPIManager.AssociarTurmaAsync(disciplinaPIId, turmaId);
        if (result)
            _logger.LogInformation("Turma associada com sucesso");
        else
            _logger.LogWarning("Falha ao associar turma - associação já existe");

        return result;
    }

    public async Task<bool> RemoverTurmaAsync(Guid disciplinaPIId, Guid turmaId)
    {
        _logger.LogInformation("Removendo turma {TurmaId} da disciplina PI {DisciplinaPIId}", turmaId, disciplinaPIId);

        var result = await _disciplinaPIManager.RemoverTurmaAsync(disciplinaPIId, turmaId);
        if (result)
            _logger.LogInformation("Turma removida com sucesso");
        else
            _logger.LogWarning("Falha ao remover turma");

        return result;
    }
}
