using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.Projetos;
using PeiFeira.Communication.Responses.Projetos;

namespace PeiFeira.Application.Services.Projetos;

public class ProjetoAppService
{
    private readonly IProjetoManager _projetoManager;
    private readonly ILogger<ProjetoAppService> _logger;

    public ProjetoAppService(
        IProjetoManager projetoManager,
        ILogger<ProjetoAppService> logger)
    {
        _projetoManager = projetoManager;
        _logger = logger;
    }

    public async Task<ProjetoResponse> CriarAsync(CreateProjetoRequest request)
    {
        _logger.LogInformation("Iniciando criação de projeto: {Titulo}, Equipe: {EquipeId}, DisciplinaPI: {DisciplinaPIId}",
            request.Titulo, request.EquipeId, request.DisciplinaPIId);
        var response = await _projetoManager.CreateAsync(request);
        _logger.LogInformation("Projeto criado com sucesso. ID: {Id}", response.Id);
        return response;
    }

    public async Task<ProjetoResponse> AtualizarAsync(Guid id, UpdateProjetoRequest request)
    {
        _logger.LogInformation("Iniciando atualização de projeto. ID: {Id}", id);
        var response = await _projetoManager.UpdateAsync(id, request);
        _logger.LogInformation("Projeto atualizado com sucesso. ID: {Id}", id);
        return response;
    }

    public async Task<bool> ExcluirAsync(Guid id)
    {
        _logger.LogInformation("Iniciando exclusão de projeto. ID: {Id}", id);
        var result = await _projetoManager.DeleteAsync(id);
        if (result)
            _logger.LogInformation("Projeto excluído com sucesso. ID: {Id}", id);
        else
            _logger.LogWarning("Falha ao excluir projeto. ID: {Id}", id);
        return result;
    }

    public async Task<ProjetoResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando projeto por ID: {Id}", id);
        return await _projetoManager.GetByIdAsync(id);
    }

    public async Task<ProjetoDetailResponse?> BuscarPorIdComDetalhesAsync(Guid id)
    {
        _logger.LogInformation("Buscando projeto com detalhes por ID: {Id}", id);
        return await _projetoManager.GetByIdWithDetailsAsync(id);
    }

    public async Task<IEnumerable<ProjetoResponse>> ListarTodosAsync()
    {
        _logger.LogInformation("Listando todos os projetos");
        return await _projetoManager.GetAllAsync();
    }

    public async Task<IEnumerable<ProjetoResponse>> ListarAtivosAsync()
    {
        _logger.LogInformation("Listando projetos ativos");
        return await _projetoManager.GetActiveAsync();
    }

    public async Task<ProjetoResponse?> BuscarPorEquipeAsync(Guid equipeId)
    {
        _logger.LogInformation("Buscando projeto da equipe: {EquipeId}", equipeId);
        return await _projetoManager.GetByEquipeIdAsync(equipeId);
    }

    public async Task<IEnumerable<ProjetoResponse>> ListarPorDisciplinaPIAsync(Guid disciplinaPIId)
    {
        _logger.LogInformation("Listando projetos da DisciplinaPI: {DisciplinaPIId}", disciplinaPIId);
        return await _projetoManager.GetByDisciplinaPIIdAsync(disciplinaPIId);
    }

    public async Task<IEnumerable<ProjetoResponse>> ListarProjetosComEmpresaAsync()
    {
        _logger.LogInformation("Listando projetos com empresa");
        return await _projetoManager.GetProjetosComEmpresaAsync();
    }

    public async Task<IEnumerable<ProjetoResponse>> ListarProjetosAcademicosAsync()
    {
        _logger.LogInformation("Listando projetos acadêmicos");
        return await _projetoManager.GetProjetosAcademicosAsync();
    }
}
