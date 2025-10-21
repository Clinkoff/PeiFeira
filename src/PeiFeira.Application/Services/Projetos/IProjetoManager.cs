using PeiFeira.Communication.Requests.Projetos;
using PeiFeira.Communication.Responses.Projetos;

namespace PeiFeira.Application.Services.Projetos;

public interface IProjetoManager
{
    Task<ProjetoResponse> CreateAsync(CreateProjetoRequest request);
    Task<ProjetoResponse> UpdateAsync(Guid id, UpdateProjetoRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<ProjetoResponse?> GetByIdAsync(Guid id);
    Task<ProjetoDetailResponse?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<ProjetoResponse>> GetAllAsync();
    Task<IEnumerable<ProjetoResponse>> GetActiveAsync();
    Task<ProjetoResponse?> GetByEquipeIdAsync(Guid equipeId);
    Task<IEnumerable<ProjetoResponse>> GetByDisciplinaPIIdAsync(Guid disciplinaPIId);
    Task<IEnumerable<ProjetoResponse>> GetProjetosComEmpresaAsync();
    Task<IEnumerable<ProjetoResponse>> GetProjetosAcademicosAsync();
}
