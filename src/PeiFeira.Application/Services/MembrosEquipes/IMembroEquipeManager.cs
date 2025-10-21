using PeiFeira.Communication.Requests.MembroEquipe;
using PeiFeira.Communication.Responses.MembroEquipe;

namespace PeiFeira.Application.Services.MembrosEquipes;

public interface IMembroEquipeManager
{
    Task<MembroEquipeResponse> AddMembroAsync(AddMembroEquipeRequest request);
    Task<bool> RemoveMembroAsync(Guid equipeId, Guid perfilAlunoId);
    Task<IEnumerable<MembroEquipeResponse>> GetByEquipeIdAsync(Guid equipeId);
    Task<IEnumerable<MembroEquipeResponse>> GetByPerfilAlunoIdAsync(Guid perfilAlunoId);
    Task<MembroEquipeResponse?> GetByIdAsync(Guid id);
    Task<bool> IsMembroAtivo(Guid equipeId, Guid perfilAlunoId);
}
