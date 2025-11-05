using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.DisciplinasPI;
using PeiFeira.Domain.Entities.Turmas;
using PeiFeira.Domain.Interfaces.Turmas;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class AlunoTurmaRepository : BaseRepository<AlunoTurma>, IAlunoTurmaRepository
{
    public AlunoTurmaRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(Guid turmaId)
    {
        return await _context.AlunosTurma
            .Include(a => a.PerfilAluno)
                .ThenInclude(p => p.Usuario)
            .Include(a => a.Turma)
            .Where(a => a.TurmaId == turmaId && a.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<AlunoTurma>> GetByPerfilAlunoIdAsync(Guid perfilAlunoId)
    {
        return await _dbSet
            .Include(at => at.Turma)
                .ThenInclude(t => t.Semestre)
            .Where(at => at.PerfilAlunoId == perfilAlunoId)
            .OrderByDescending(at => at.DataMatricula)
            .ToListAsync();
    }

    public async Task<AlunoTurma?> GetMatriculaAtualByPerfilAlunoIdAsync(Guid perfilAlunoId)
    {
        return await _dbSet
            .Include(at => at.Turma)
                .ThenInclude(t => t.Semestre)
            .FirstOrDefaultAsync(at =>
                at.PerfilAlunoId == perfilAlunoId &&
                at.IsAtual == true);
    }

    public async Task<bool> ExistsMatriculaAtivaAsync(Guid perfilAlunoId, Guid turmaId)
    {
        return await _dbSet.AnyAsync(at =>
            at.PerfilAlunoId == perfilAlunoId &&
            at.TurmaId == turmaId &&
            at.IsAtual == true);
    }

    public async Task<bool> AlunoEstaEmAlgumaTurmaDaDisciplinaAsync(Guid perfilAlunoId, Guid disciplinaPIId)
    {
        // Busca turmas da DisciplinaPI
        var turmaIds = await _context.Set<DisciplinaPITurma>()
            .Where(dt => dt.DisciplinaPIId == disciplinaPIId)
            .Select(dt => dt.TurmaId)
            .ToListAsync();

        // Verifica se aluno está matriculado em alguma dessas turmas
        return await _dbSet.AnyAsync(at =>
            at.PerfilAlunoId == perfilAlunoId &&
            turmaIds.Contains(at.TurmaId) &&
            at.IsAtual == true);
    }
}