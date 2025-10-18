using PeiFeira.Communication.Requests.DisciplinaPI;
using PeiFeira.Communication.Responses.DisciplinaPI;

namespace PeiFeira.Application.Services.DisciplinasPI;

public interface IDisciplinaPIManager
{
    Task<DisciplinaPIResponse> CreateAsync(CreateDisciplinaPIRequest request);
    Task<DisciplinaPIResponse> UpdateAsync(Guid id, UpdateDisciplinaPIRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<DisciplinaPIResponse?> GetByIdAsync(Guid id);
    Task<DisciplinaPIDetailResponse?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<DisciplinaPIResponse>> GetAllAsync();
    Task<IEnumerable<DisciplinaPIResponse>> GetActiveAsync();
    Task<IEnumerable<DisciplinaPIResponse>> GetBySemestreIdAsync(Guid semestreId);
    Task<IEnumerable<DisciplinaPIResponse>> GetByProfessorIdAsync(Guid perfilProfessorId);
    Task<IEnumerable<DisciplinaPIResponse>> GetByTurmaIdAsync(Guid turmaId);
    Task<bool> ExistsByNomeAndSemestreAsync(string nome, Guid semestreId);
    Task<bool> AssociarTurmaAsync(Guid disciplinaPIId, Guid turmaId);
    Task<bool> RemoverTurmaAsync(Guid disciplinaPIId, Guid turmaId);
}
