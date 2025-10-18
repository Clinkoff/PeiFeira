using PeiFeira.Domain.Entities.Equipes;

namespace PeiFeira.Domain.Interfaces.Equipes;

public interface IMembroEquipeRepository : IBaseRepository<MembroEquipe>
{
    Task<IEnumerable<MembroEquipe>> GetByEquipeIdAsync(Guid equipeId);
    Task<IEnumerable<MembroEquipe>> GetByUsuarioIdAsync(Guid usuarioId);
    Task<MembroEquipe?> GetByEquipeAndUsuarioAsync(Guid equipeId, Guid usuarioId);

    Task<bool> IsUsuarioInEquipeAsync(Guid equipeId, Guid usuarioId);
    Task<bool> UsuarioJaEstaEmEquipeAsync(Guid usuarioId);
    Task<int> CountMembrosAtivosAsync(Guid equipeId);

    Task<bool> RemoveFromEquipeAsync(Guid equipeId, Guid usuarioId);
    Task<MembroEquipe?> GetLiderEquipeAsync(Guid equipeId);
    Task<IEnumerable<MembroEquipe>> GetMembrosComUsuarioAsync(Guid equipeId); // Com include do Usuario
}