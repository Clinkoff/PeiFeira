using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.DisciplinasPI;
using PeiFeira.Domain.Interfaces.DisciplinasPI;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class DisciplinaPITurmaRepository : BaseRepository<DisciplinaPITurma>, IDisciplinaPITurmaRepository
{
    public DisciplinaPITurmaRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<DisciplinaPITurma>> GetByDisciplinaPIIdAsync(Guid disciplinaPIId)
    {
        return await _dbSet
            .Include(dt => dt.Turma)
            .Include(dt => dt.DisciplinaPI)
            .Where(dt => dt.DisciplinaPIId == disciplinaPIId)
            .ToListAsync();
    }

    public async Task<IEnumerable<DisciplinaPITurma>> GetByTurmaIdAsync(Guid turmaId)
    {
        return await _dbSet
            .Include(dt => dt.DisciplinaPI)
                .ThenInclude(d => d.Professor)
                    .ThenInclude(p => p.Usuario)
            .Include(dt => dt.DisciplinaPI)
                .ThenInclude(d => d.Semestre)
            .Include(dt => dt.Turma)
            .Where(dt => dt.TurmaId == turmaId)
            .ToListAsync();
    }

    public async Task<DisciplinaPITurma?> GetByDisciplinaPIIdAndTurmaIdAsync(Guid disciplinaPIId, Guid turmaId)
    {
        return await _dbSet
            .Include(dt => dt.DisciplinaPI)
            .Include(dt => dt.Turma)
            .FirstOrDefaultAsync(dt => dt.DisciplinaPIId == disciplinaPIId && dt.TurmaId == turmaId);
    }

    public async Task<bool> ExistsByDisciplinaPIIdAndTurmaIdAsync(Guid disciplinaPIId, Guid turmaId)
    {
        return await _dbSet.AnyAsync(dt => dt.DisciplinaPIId == disciplinaPIId && dt.TurmaId == turmaId);
    }

    public async Task<bool> DeleteByDisciplinaPIIdAndTurmaIdAsync(Guid disciplinaPIId, Guid turmaId)
    {
        var entity = await GetByDisciplinaPIIdAndTurmaIdAsync(disciplinaPIId, turmaId);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        return true;
    }
}
