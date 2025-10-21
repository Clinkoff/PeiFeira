using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.ConviteEquipe;
using PeiFeira.Communication.Responses.ConviteEquipe;

namespace PeiFeira.Application.Services.ConviteEquipe;

public class ConviteEquipeAppService
{
    private readonly IConviteEquipeManager _conviteEquipeManager;
    private readonly ILogger<ConviteEquipeAppService> _logger;

    public ConviteEquipeAppService(
        IConviteEquipeManager conviteEquipeManager,
        ILogger<ConviteEquipeAppService> logger)
    {
        _conviteEquipeManager = conviteEquipeManager;
        _logger = logger;
    }

    public async Task<ConviteEquipeResponse> EnviarConviteAsync(CreateConviteEquipeRequest request)
    {
        _logger.LogInformation("Enviando convite de equipe. EquipeId: {EquipeId}, ConvidadoPorId: {ConvidadoPorId}, ConvidadoId: {ConvidadoId}",
            request.EquipeId, request.ConvidadoPorId, request.ConvidadoId);
        var response = await _conviteEquipeManager.EnviarConviteAsync(request);
        _logger.LogInformation("Convite enviado com sucesso. ID: {Id}", response.Id);
        return response;
    }

    public async Task<ConviteEquipeResponse> AceitarConviteAsync(Guid conviteId, Guid perfilAlunoId)
    {
        _logger.LogInformation("Aceitando convite. ConviteId: {ConviteId}, PerfilAlunoId: {PerfilAlunoId}",
            conviteId, perfilAlunoId);
        var response = await _conviteEquipeManager.AceitarConviteAsync(conviteId, perfilAlunoId);
        _logger.LogInformation("Convite aceito com sucesso. ConviteId: {ConviteId}", conviteId);
        return response;
    }

    public async Task<ConviteEquipeResponse> RecusarConviteAsync(Guid conviteId, Guid perfilAlunoId)
    {
        _logger.LogInformation("Recusando convite. ConviteId: {ConviteId}, PerfilAlunoId: {PerfilAlunoId}",
            conviteId, perfilAlunoId);
        var response = await _conviteEquipeManager.RecusarConviteAsync(conviteId, perfilAlunoId);
        _logger.LogInformation("Convite recusado. ConviteId: {ConviteId}", conviteId);
        return response;
    }

    public async Task<ConviteEquipeResponse> CancelarConviteAsync(Guid conviteId, Guid perfilAlunoId)
    {
        _logger.LogInformation("Cancelando convite. ConviteId: {ConviteId}, PerfilAlunoId: {PerfilAlunoId}",
            conviteId, perfilAlunoId);
        var response = await _conviteEquipeManager.CancelarConviteAsync(conviteId, perfilAlunoId);
        _logger.LogInformation("Convite cancelado. ConviteId: {ConviteId}", conviteId);
        return response;
    }

    public async Task<ConviteEquipeResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando convite por ID: {Id}", id);
        return await _conviteEquipeManager.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ConviteEquipeResponse>> ListarConvitesPendentesAsync(Guid perfilAlunoId)
    {
        _logger.LogInformation("Listando convites pendentes do aluno: {PerfilAlunoId}", perfilAlunoId);
        return await _conviteEquipeManager.GetConvitesPendentesAsync(perfilAlunoId);
    }

    public async Task<IEnumerable<ConviteEquipeResponse>> ListarConvitesPorEquipeAsync(Guid equipeId)
    {
        _logger.LogInformation("Listando convites da equipe: {EquipeId}", equipeId);
        return await _conviteEquipeManager.GetConvitesByEquipeAsync(equipeId);
    }
}
