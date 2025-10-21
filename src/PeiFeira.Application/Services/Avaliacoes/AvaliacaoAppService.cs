using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.Avaliacoes;
using PeiFeira.Communication.Responses.Avaliacoes;

namespace PeiFeira.Application.Services.Avaliacoes;

public class AvaliacaoAppService
{
    private readonly IAvaliacaoManager _avaliacaoManager;
    private readonly ILogger<AvaliacaoAppService> _logger;

    public AvaliacaoAppService(
        IAvaliacaoManager avaliacaoManager,
        ILogger<AvaliacaoAppService> logger)
    {
        _avaliacaoManager = avaliacaoManager;
        _logger = logger;
    }

    public async Task<AvaliacaoResponse> CriarAsync(CreateAvaliacaoRequest request)
    {
        _logger.LogInformation("Iniciando criação de avaliação: Equipe: {EquipeId}, Avaliador: {AvaliadorId}",
            request.EquipeId, request.AvaliadorId);
        var response = await _avaliacaoManager.CreateAsync(request);
        _logger.LogInformation("Avaliação criada com sucesso. ID: {Id}, Nota Final: {NotaFinal}",
            response.Id, response.NotaFinal);
        return response;
    }

    public async Task<AvaliacaoResponse> AtualizarAsync(Guid id, UpdateAvaliacaoRequest request)
    {
        _logger.LogInformation("Iniciando atualização de avaliação. ID: {Id}", id);
        var response = await _avaliacaoManager.UpdateAsync(id, request);
        _logger.LogInformation("Avaliação atualizada com sucesso. ID: {Id}, Nova Nota Final: {NotaFinal}",
            id, response.NotaFinal);
        return response;
    }

    public async Task<bool> ExcluirAsync(Guid id)
    {
        _logger.LogInformation("Iniciando exclusão de avaliação. ID: {Id}", id);
        var result = await _avaliacaoManager.DeleteAsync(id);
        if (result)
            _logger.LogInformation("Avaliação excluída com sucesso. ID: {Id}", id);
        else
            _logger.LogWarning("Falha ao excluir avaliação. ID: {Id}", id);
        return result;
    }

    public async Task<AvaliacaoResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando avaliação por ID: {Id}", id);
        return await _avaliacaoManager.GetByIdAsync(id);
    }

    public async Task<IEnumerable<AvaliacaoResponse>> ListarTodasAsync()
    {
        _logger.LogInformation("Listando todas as avaliações");
        return await _avaliacaoManager.GetAllAsync();
    }

    public async Task<IEnumerable<AvaliacaoResponse>> ListarPorEquipeAsync(Guid equipeId)
    {
        _logger.LogInformation("Listando avaliações da equipe: {EquipeId}", equipeId);
        return await _avaliacaoManager.GetByEquipeIdAsync(equipeId);
    }

    public async Task<IEnumerable<AvaliacaoResponse>> ListarPorAvaliadorAsync(Guid avaliadorId)
    {
        _logger.LogInformation("Listando avaliações do avaliador: {AvaliadorId}", avaliadorId);
        return await _avaliacaoManager.GetByAvaliadorIdAsync(avaliadorId);
    }

    public async Task<decimal> ObterMediaEquipeAsync(Guid equipeId)
    {
        _logger.LogInformation("Calculando média da equipe: {EquipeId}", equipeId);
        var media = await _avaliacaoManager.GetMediaEquipeAsync(equipeId);
        _logger.LogInformation("Média da equipe {EquipeId}: {Media}", equipeId, media);
        return media;
    }

    public async Task<decimal> ObterMediaGeralAsync()
    {
        _logger.LogInformation("Calculando média geral de todas as avaliações");
        var media = await _avaliacaoManager.GetMediaGeralAsync();
        _logger.LogInformation("Média geral: {Media}", media);
        return media;
    }

    public async Task<IEnumerable<AvaliacaoResponse>> ListarPorFaixaNotaAsync(decimal notaMin, decimal notaMax)
    {
        _logger.LogInformation("Listando avaliações na faixa de nota: {NotaMin} a {NotaMax}", notaMin, notaMax);
        return await _avaliacaoManager.GetAvaliacoesPorFaixaNotaAsync(notaMin, notaMax);
    }
}
