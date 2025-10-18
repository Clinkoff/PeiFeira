using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.DisciplinasPI;
using PeiFeira.Domain.Interfaces.DisciplinasPI;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class DisciplinaPIRepository : BaseRepository<DisciplinaPI>, IDisciplinaPIRepository
{
    public DisciplinaPIRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<DisciplinaPI?> GetByIdWithIncludesAsync(Guid id)
    {
        return await _dbSet
            .Include(d => d.Semestre)
            .Include(d => d.Professor)
                .ThenInclude(p => p.Usuario)
            .Include(d => d.DisciplinaPITurmas)
                .ThenInclude(dt => dt.Turma)
            .Include(d => d.Projetos)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<DisciplinaPI>> GetBySemestreIdAsync(Guid semestreId)
    {
        return await _dbSet
            .Include(d => d.Professor)
                .ThenInclude(p => p.Usuario)
            .Include(d => d.DisciplinaPITurmas)
                .ThenInclude(dt => dt.Turma)
            .Where(d => d.SemestreId == semestreId)
            .ToListAsync();
    }

    public async Task<IEnumerable<DisciplinaPI>> GetByProfessorIdAsync(Guid perfilProfessorId)
    {
        return await _dbSet
            .Include(d => d.Semestre)
            .Include(d => d.DisciplinaPITurmas)
                .ThenInclude(dt => dt.Turma)
            .Where(d => d.PerfilProfessorId == perfilProfessorId)
            .ToListAsync();
    }

    public async Task<IEnumerable<DisciplinaPI>> GetAtivasByTurmaIdAsync(Guid turmaId)
    {
        return await _dbSet
            .Include(d => d.Professor)
                .ThenInclude(p => p.Usuario)
            .Include(d => d.Semestre)
            .Include(d => d.DisciplinaPITurmas)
                .ThenInclude(dt => dt.Turma)
            .Where(d => d.DisciplinaPITurmas.Any(dt => dt.TurmaId == turmaId) && d.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<DisciplinaPI>> GetByNomeAsync(string nome)
    {
        return await _dbSet
            .Include(d => d.Professor)
                .ThenInclude(p => p.Usuario)
            .Include(d => d.Semestre)
            .Where(d => d.Nome.Contains(nome))
            .ToListAsync();
    }

    public async Task<IEnumerable<DisciplinaPI>> GetAtivasAsync()
    {
        return await _dbSet
            .Include(d => d.Professor)
                .ThenInclude(p => p.Usuario)
            .Include(d => d.Semestre)
            .Include(d => d.DisciplinaPITurmas)
                .ThenInclude(dt => dt.Turma)
            .Where(d => d.IsActive)
            .ToListAsync();
    }

    public async Task<bool> ExistsByNomeAndSemestreIdAsync(string nome, Guid semestreId)
    {
        return await _dbSet.AnyAsync(d => d.Nome == nome && d.SemestreId == semestreId);
    }
}
