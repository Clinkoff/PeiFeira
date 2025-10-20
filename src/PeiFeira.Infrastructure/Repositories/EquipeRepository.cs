using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Interfaces.Equipes;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class EquipeRepository : BaseRepository<Equipe>, IEquipeRepository
{
    public EquipeRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<Equipe?> GetByCodigoConviteAsync(string codigoConvite)
    {
        return await _dbSet
            .Include(e => e.Lider)
                .ThenInclude(l => l.Usuario)
            .Include(e => e.Membros)
                .ThenInclude(m => m.PerfilAluno)
                    .ThenInclude(pa => pa.Usuario)
            .Include(e => e.Projeto)
            .FirstOrDefaultAsync(e => e.CodigoConvite == codigoConvite && e.IsActive);
    }

    public async Task<Equipe?> GetByLiderIdAsync(Guid liderId)
    {
        return await _dbSet
            .Include(e => e.Lider)
                .ThenInclude(l => l.Usuario)
            .Include(e => e.Membros)
                .ThenInclude(m => m.PerfilAluno)
                    .ThenInclude(pa => pa.Usuario)
            .Include(e => e.Projeto)
            .FirstOrDefaultAsync(e => e.LiderPerfilAlunoId == liderId && e.IsActive);
    }

    public async Task<IEnumerable<Equipe>> GetByEquipesByUsuariosIdAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(e => e.Lider)
                .ThenInclude(l => l.Usuario)
            .Include(e => e.Membros)
                .ThenInclude(m => m.PerfilAluno)
                    .ThenInclude(pa => pa.Usuario)
            .Include(e => e.Projeto)
            .Where(e => e.Membros.Any(m => m.PerfilAluno.UsuarioId == usuarioId) && e.IsActive)
            .ToListAsync();
    }

    public async Task<Equipe?> GetWithProjetoAsync(Guid id)
    {
        return await _dbSet
            .Include(e => e.Lider)
                .ThenInclude(l => l.Usuario)
            .Include(e => e.Projeto)
                .ThenInclude(p => p!.DisciplinaPI)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Equipe?> GetWithMembrosAsync(Guid id)
    {
        return await _dbSet
            .Include(e => e.Lider)
                .ThenInclude(l => l.Usuario)
            .Include(e => e.Membros)
                .ThenInclude(m => m.PerfilAluno)
                    .ThenInclude(pa => pa.Usuario)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Equipe?> GetCompleteAsync(Guid id)
    {
        return await _dbSet
            .Include(e => e.Lider)
                .ThenInclude(l => l.Usuario)
            .Include(e => e.Membros)
                .ThenInclude(m => m.PerfilAluno)
                    .ThenInclude(pa => pa.Usuario)
            .Include(e => e.Projeto)
                .ThenInclude(p => p!.DisciplinaPI)
            .Include(e => e.Avaliacoes)
                .ThenInclude(a => a.Avaliador)
                    .ThenInclude(av => av.Usuario)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<string> GenerateCodigoConviteAsync()
    {
        string codigo;
        do
        {
            // Gera código de 6 caracteres alfanuméricos
            codigo = Guid.NewGuid().ToString("N")[..6].ToUpper();
        }
        while (await _dbSet.AnyAsync(e => e.CodigoConvite == codigo));

        return codigo;
    }

    public async Task<bool> IsCodigoConviteValidoAsync(string codigo)
    {
        return await _dbSet.AnyAsync(e => e.CodigoConvite == codigo && e.IsActive);
    }
}