using PeiFeira.Domain.Entities.Turmas;

namespace PeiFeira.Domain.Interfaces.Turmas;

public interface ITurma : IBaseRepository<Turma>
{
    Task<IEnumerable<Turma>> GetBySemestreIdAsync(Guid semestreId);
    Task<Turma?> GetByCodigoAsync(string codigo);
    Task<bool> ExistsByCodigoAsync(string codigo);
    Task<IEnumerable<Turma>> GetByCursoAsync(string curso);
}
