using PeiFeira.Communication.Requests.Turma;
using PeiFeira.Communication.Responses.Turmas;

namespace PeiFeira.Application.Services.Turmas;

public interface ITurmaManager
{
    Task<TurmaResponse> CreateAsync(CreateTurmaRequest request);
    Task<TurmaResponse> UpdateAsync(Guid id, UpdateTurmaRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<TurmaResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<TurmaResponse>> GetAllAsync();
    Task<IEnumerable<TurmaResponse>> GetActiveAsync();
    Task<IEnumerable<TurmaResponse>> GetBySemestreIdAsync(Guid semestreId);
    Task<IEnumerable<TurmaResponse>> GetByCursoAsync(string curso);
    Task<TurmaResponse?> GetByCodigoAsync(string codigo);
}
