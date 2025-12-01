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
    public override async Task<IEnumerable<Turma>> GetAllAsync()
    {
        return await _dbSet
            .Include(t => t.Semestre) 
            .Include(t => t.AlunosTurma) 
            .OrderByDescending(t => t.CriadoEm)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Turma>> GetActiveAsync()
    {
        return await _dbSet
            .Include(t => t.Semestre)
            .Where(t => t.IsActive)
            .OrderByDescending(t => t.CriadoEm)
            .ToListAsync();
    }

    public override async Task<Turma?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(t => t.Semestre)
            .Include(t => t.AlunosTurma)
                .ThenInclude(at => at.PerfilAluno)
                    .ThenInclude(pa => pa.Usuario)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}