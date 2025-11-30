using PeiFeira.Communication.Requests.Equipes;
using PeiFeira.Communication.Responses.Equipes;

namespace PeiFeira.Application.Services.Equipes;

public interface IEquipeManager
{
    Task<EquipeResponse> CreateAsync(CreateEquipeRequest request);
    Task<EquipeResponse> UpdateAsync(Guid id, UpdateEquipeRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<EquipeResponse?> GetByIdAsync(Guid id);
    Task<EquipeDetailResponse?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<EquipeResponse>> GetAllAsync();
    Task<IEnumerable<EquipeResponse>> GetActiveAsync();
    Task<EquipeResponse?> GetByLiderIdAsync(Guid liderId);
    Task<EquipeResponse?> GetByCodigoConviteAsync(string codigo);
    Task<EquipeResponse> RegenerarCodigoConviteAsync(Guid id);
    Task<IEnumerable<EquipeResponse>> ListarComProjetoAsync();
}
