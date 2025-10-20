using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.Semestres;
using PeiFeira.Communication.Responses.Semestres;

namespace PeiFeira.Application.Services.Semestres;

public class SemestreAppService
{
    private readonly ISemestreManager _semestreManager;
    private readonly ILogger<SemestreAppService> _logger;

    public SemestreAppService(
        ISemestreManager semestreManager,
        ILogger<SemestreAppService> logger)
    {
        _semestreManager = semestreManager;
        _logger = logger;
    }

    public async Task<SemestreResponse> CriarAsync(CreateSemestreRequest request)
    {
        _logger.LogInformation("Iniciando criação de semestre: {Nome}", request.Nome);
        var response = await _semestreManager.CreateAsync(request);
        _logger.LogInformation("Semestre criado com sucesso. ID: {Id}", response.Id);
        return response;
    }

    public async Task<SemestreResponse> AtualizarAsync(Guid id, UpdateSemestreRequest request)
    {
        _logger.LogInformation("Iniciando atualização de semestre. ID: {Id}", id);
        var response = await _semestreManager.UpdateAsync(id, request);
        _logger.LogInformation("Semestre atualizado com sucesso. ID: {Id}", id);
        return response;
    }

    public async Task<bool> ExcluirAsync(Guid id)
    {
        _logger.LogInformation("Iniciando exclusão de semestre. ID: {Id}", id);
        var result = await _semestreManager.DeleteAsync(id);
        if (result)
            _logger.LogInformation("Semestre excluído com sucesso. ID: {Id}", id);
        else
            _logger.LogWarning("Falha ao excluir semestre. ID: {Id}", id);
        return result;
    }

    public async Task<SemestreResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando semestre por ID: {Id}", id);
        return await _semestreManager.GetByIdAsync(id);
    }

    public async Task<IEnumerable<SemestreResponse>> ListarTodosAsync()
    {
        _logger.LogInformation("Listando todos os semestres");
        return await _semestreManager.GetAllAsync();
    }

    public async Task<IEnumerable<SemestreResponse>> ListarAtivosAsync()
    {
        _logger.LogInformation("Listando semestres ativos");
        return await _semestreManager.GetActiveAsync();
    }

    public async Task<IEnumerable<SemestreResponse>> ListarPorAnoAsync(int ano)
    {
        _logger.LogInformation("Listando semestres do ano: {Ano}", ano);
        return await _semestreManager.GetByAnoAsync(ano);
    }

    public async Task<SemestreResponse?> BuscarPorAnoEPeriodoAsync(int ano, int periodo)
    {
        _logger.LogInformation("Buscando semestre: {Ano}.{Periodo}", ano, periodo);
        return await _semestreManager.GetByAnoAndPeriodoAsync(ano, periodo);
    }
}