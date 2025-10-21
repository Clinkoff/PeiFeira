using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.Equipes;
using PeiFeira.Communication.Responses.Equipes;

namespace PeiFeira.Application.Services.Equipes;

public class EquipeAppService
{
    private readonly IEquipeManager _equipeManager;
    private readonly ILogger<EquipeAppService> _logger;

    public EquipeAppService(
        IEquipeManager equipeManager,
        ILogger<EquipeAppService> logger)
    {
        _equipeManager = equipeManager;
        _logger = logger;
    }

    public async Task<EquipeResponse> CriarAsync(CreateEquipeRequest request)
    {
        _logger.LogInformation("Iniciando criação de equipe: {Nome}", request.Nome);
        var response = await _equipeManager.CreateAsync(request);
        _logger.LogInformation("Equipe criada com sucesso. ID: {Id}", response.Id);
        return response;
    }

    public async Task<EquipeResponse> AtualizarAsync(Guid id, UpdateEquipeRequest request)
    {
        _logger.LogInformation("Iniciando atualização de equipe. ID: {Id}", id);
        var response = await _equipeManager.UpdateAsync(id, request);
        _logger.LogInformation("Equipe atualizada com sucesso. ID: {Id}", id);
        return response;
    }

    public async Task<bool> ExcluirAsync(Guid id)
    {
        _logger.LogInformation("Iniciando exclusão de equipe. ID: {Id}", id);
        var result = await _equipeManager.DeleteAsync(id);
        if (result)
            _logger.LogInformation("Equipe excluída com sucesso. ID: {Id}", id);
        else
            _logger.LogWarning("Falha ao excluir equipe. ID: {Id}", id);
        return result;
    }

    public async Task<EquipeResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando equipe por ID: {Id}", id);
        return await _equipeManager.GetByIdAsync(id);
    }

    public async Task<EquipeDetailResponse?> BuscarPorIdComDetalhesAsync(Guid id)
    {
        _logger.LogInformation("Buscando equipe com detalhes por ID: {Id}", id);
        return await _equipeManager.GetByIdWithDetailsAsync(id);
    }

    public async Task<IEnumerable<EquipeResponse>> ListarTodasAsync()
    {
        _logger.LogInformation("Listando todas as equipes");
        return await _equipeManager.GetAllAsync();
    }

    public async Task<IEnumerable<EquipeResponse>> ListarAtivasAsync()
    {
        _logger.LogInformation("Listando equipes ativas");
        return await _equipeManager.GetActiveAsync();
    }

    public async Task<EquipeResponse?> BuscarPorLiderAsync(Guid liderId)
    {
        _logger.LogInformation("Buscando equipe por líder ID: {LiderId}", liderId);
        return await _equipeManager.GetByLiderIdAsync(liderId);
    }

    public async Task<EquipeResponse?> BuscarPorCodigoAsync(string codigo)
    {
        _logger.LogInformation("Buscando equipe por código de convite: {Codigo}", codigo);
        return await _equipeManager.GetByCodigoConviteAsync(codigo);
    }

    public async Task<EquipeResponse> RegenerarCodigoAsync(Guid id)
    {
        _logger.LogInformation("Regenerando código de convite da equipe. ID: {Id}", id);
        var response = await _equipeManager.RegenerarCodigoConviteAsync(id);
        _logger.LogInformation("Código de convite regenerado com sucesso. ID: {Id}, Novo código: {Codigo}", id, response.CodigoConvite);
        return response;
    }
}
