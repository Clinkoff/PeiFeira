using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Interfaces.Projetos;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class ProjetoRepository : BaseRepository<Projeto>, IProjetoRepository
{
    public ProjetoRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<Projeto?> GetByEquipeIdAsync(Guid equipeId)
    {
        return await _dbSet
            .Include(p => p.Equipe)
                .ThenInclude(e => e.Lider)
                    .ThenInclude(l => l.Usuario)
            .Include(p => p.Equipe)
                .ThenInclude(e => e.Membros)
                    .ThenInclude(m => m.PerfilAluno)
                        .ThenInclude(pa => pa.Usuario)
            .Include(p => p.DisciplinaPI)
                .ThenInclude(d => d.Professor)
                    .ThenInclude(prof => prof.Usuario)
            .FirstOrDefaultAsync(p => p.EquipeId == equipeId);
    }

    public async Task<bool> EquipeJaTemProjetoAsync(Guid equipeId)
    {
        return await _dbSet.AnyAsync(p => p.EquipeId == equipeId && p.IsActive);
    }

    public async Task<IEnumerable<Projeto>> GetByTemaAsync(string tema)
    {
        return await _dbSet
            .Include(p => p.Equipe)
            .Include(p => p.DisciplinaPI)
            .Where(p => p.DesafioProposto.Contains(tema) || p.Titulo.Contains(tema))
            .ToListAsync();
    }

    public async Task<IEnumerable<Projeto>> SearchByTituloAsync(string titulo)
    {
        return await _dbSet
            .Include(p => p.Equipe)
            .Include(p => p.DisciplinaPI)
            .Where(p => p.Titulo.Contains(titulo))
            .ToListAsync();
    }

    public async Task<IEnumerable<Projeto>> GetProjetosComEmpresaAsync()
    {
        return await _dbSet
            .Include(p => p.Equipe)
            .Include(p => p.DisciplinaPI)
            .Where(p => !string.IsNullOrEmpty(p.NomeEmpresa))
            .ToListAsync();
    }

    public async Task<IEnumerable<Projeto>> GetProjetosAcademicosAsync()
    {
        return await _dbSet
            .Include(p => p.Equipe)
            .Include(p => p.DisciplinaPI)
            .Where(p => string.IsNullOrEmpty(p.NomeEmpresa))
            .ToListAsync();
    }

    public async Task<IEnumerable<Projeto>> GetByEmpresaAsync(string nomeEmpresa)
    {
        return await _dbSet
            .Include(p => p.Equipe)
            .Include(p => p.DisciplinaPI)
            .Where(p => p.NomeEmpresa != null && p.NomeEmpresa.Contains(nomeEmpresa))
            .ToListAsync();
    }

    public async Task<Projeto?> GetWithEquipeAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Equipe)
                .ThenInclude(e => e.Lider)
                    .ThenInclude(l => l.Usuario)
            .Include(p => p.Equipe)
                .ThenInclude(e => e.Membros)
                    .ThenInclude(m => m.PerfilAluno)
                        .ThenInclude(pa => pa.Usuario)
            .Include(p => p.DisciplinaPI)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}