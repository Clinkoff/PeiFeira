using PeiFeira.Domain.Entities.DisciplinasPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeiFeira.Domain.Interfaces.DisciplinasPI;

public interface IDisciplinaPIRepository : IBaseRepository<DisciplinaPI>
{
    Task<DisciplinaPI?> GetByIdWithIncludesAsync(Guid id);
    Task<IEnumerable<DisciplinaPI>> GetBySemestreIdAsync(Guid semestreId);
    Task<IEnumerable<DisciplinaPI>> GetByProfessorIdAsync(Guid perfilProfessorId);
    Task<IEnumerable<DisciplinaPI>> GetAtivasByTurmaIdAsync(Guid turmaId);
    Task<IEnumerable<DisciplinaPI>> GetByNomeAsync(string nome);
    Task<IEnumerable<DisciplinaPI>> GetAtivasAsync();
    Task<bool> ExistsByNomeAndSemestreIdAsync(string nome, Guid semestreId);
}
