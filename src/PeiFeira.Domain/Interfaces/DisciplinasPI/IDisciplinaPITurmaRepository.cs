using PeiFeira.Domain.Entities.DisciplinasPI;

namespace PeiFeira.Domain.Interfaces.DisciplinasPI;

public interface IDisciplinaPITurmaRepository : IBaseRepository<DisciplinaPITurma>
{
    Task<IEnumerable<DisciplinaPITurma>> GetByDisciplinaPIIdAsync(Guid disciplinaPIId);
    Task<IEnumerable<DisciplinaPITurma>> GetByTurmaIdAsync(Guid turmaId);
    Task<DisciplinaPITurma?> GetByDisciplinaPIIdAndTurmaIdAsync(Guid disciplinaPIId, Guid turmaId);
    Task<bool> ExistsByDisciplinaPIIdAndTurmaIdAsync(Guid disciplinaPIId, Guid turmaId);
    Task<bool> DeleteByDisciplinaPIIdAndTurmaIdAsync(Guid disciplinaPIId, Guid turmaId);
}
