using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Turmas;
using PeiFeira.Domain.Interfaces.Turmas;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class TurmaRepository : BaseRepository<Turma>, ITurma
{
    public TurmaRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Turma>> GetBySemestreIdAsync(Guid semestreId)
    {
        return await _dbSet
            .Include(t => t.Semestre)
            .Where(t => t.SemestreId == semestreId)
            .ToListAsync();
    }

    public async Task<Turma?> GetByCodigoAsync(string codigo)
    {
        return await _dbSet
            .Include(t => t.Semestre)
            .FirstOrDefaultAsync(t => t.Codigo == codigo);
    }

    public async Task<bool> ExistsByCodigoAsync(string codigo)
    {
        return await _dbSet.AnyAsync(t => t.Codigo == codigo);
    }

    public async Task<IEnumerable<Turma>> GetByCursoAsync(string curso)
    {
        return await _dbSet
            .Include(t => t.Semestre)
            .Where(t => t.Curso != null && t.Curso.Contains(curso))
            .ToListAsync();
    }
}