using Microsoft.Extensions.Logging;
using PeiFeira.Communication.Requests.MembroEquipe;
using PeiFeira.Communication.Responses.MembroEquipe;

namespace PeiFeira.Application.Services.MembrosEquipes;

public class MembroEquipeAppService
{
    private readonly IMembroEquipeManager _membroEquipeManager;
    private readonly ILogger<MembroEquipeAppService> _logger;

    public MembroEquipeAppService(
        IMembroEquipeManager membroEquipeManager,
        ILogger<MembroEquipeAppService> logger)
    {
        _membroEquipeManager = membroEquipeManager;
        _logger = logger;
    }

    public async Task<MembroEquipeResponse> AdicionarMembroAsync(AddMembroEquipeRequest request)
    {
        _logger.LogInformation("Iniciando adição de membro à equipe. EquipeId: {EquipeId}, PerfilAlunoId: {PerfilAlunoId}",
            request.EquipeId, request.PerfilAlunoId);
        var response = await _membroEquipeManager.AddMembroAsync(request);
        _logger.LogInformation("Membro adicionado com sucesso. ID: {Id}", response.Id);
        return response;
    }

    public async Task<bool> RemoverMembroAsync(Guid equipeId, Guid perfilAlunoId)
    {
        _logger.LogInformation("Iniciando remoção de membro. EquipeId: {EquipeId}, PerfilAlunoId: {PerfilAlunoId}",
            equipeId, perfilAlunoId);
        var result = await _membroEquipeManager.RemoveMembroAsync(equipeId, perfilAlunoId);
        if (result)
            _logger.LogInformation("Membro removido com sucesso. EquipeId: {EquipeId}, PerfilAlunoId: {PerfilAlunoId}",
                equipeId, perfilAlunoId);
        else
            _logger.LogWarning("Falha ao remover membro. EquipeId: {EquipeId}, PerfilAlunoId: {PerfilAlunoId}",
                equipeId, perfilAlunoId);
        return result;
    }

    public async Task<IEnumerable<MembroEquipeResponse>> ListarMembrosPorEquipeAsync(Guid equipeId)
    {
        _logger.LogInformation("Listando membros da equipe: {EquipeId}", equipeId);
        return await _membroEquipeManager.GetByEquipeIdAsync(equipeId);
    }

    public async Task<IEnumerable<MembroEquipeResponse>> ListarEquipesPorAlunoAsync(Guid perfilAlunoId)
    {
        _logger.LogInformation("Listando equipes do aluno: {PerfilAlunoId}", perfilAlunoId);
        return await _membroEquipeManager.GetByPerfilAlunoIdAsync(perfilAlunoId);
    }

    public async Task<MembroEquipeResponse?> BuscarPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando membro de equipe por ID: {Id}", id);
        return await _membroEquipeManager.GetByIdAsync(id);
    }

    public async Task<bool> VerificarSeMembroAsync(Guid equipeId, Guid perfilAlunoId)
    {
        _logger.LogInformation("Verificando se aluno é membro. EquipeId: {EquipeId}, PerfilAlunoId: {PerfilAlunoId}",
            equipeId, perfilAlunoId);
        return await _membroEquipeManager.IsMembroAtivo(equipeId, perfilAlunoId);
    }
}
