using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Interfaces.Equipes;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class MembroEquipeRepository : BaseRepository<MembroEquipe>, IMembroEquipeRepository
{
    public MembroEquipeRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MembroEquipe>> GetByEquipeIdAsync(Guid equipeId)
    {
        return await _dbSet
            .Include(m => m.PerfilAluno)
                .ThenInclude(pa => pa.Usuario)
            .Include(m => m.Equipe)
            .Where(m => m.EquipeId == equipeId && m.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<MembroEquipe>> GetByUsuarioIdAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(m => m.Equipe)
            .Include(m => m.PerfilAluno)
                .ThenInclude(pa => pa.Usuario)
            .Where(m => m.PerfilAluno.UsuarioId == usuarioId && m.IsActive)
            .ToListAsync();
    }

    public async Task<MembroEquipe?> GetByEquipeAndUsuarioAsync(Guid equipeId, Guid usuarioId)
    {
        return await _dbSet
            .Include(m => m.PerfilAluno)
                .ThenInclude(pa => pa.Usuario)
            .Include(m => m.Equipe)
            .FirstOrDefaultAsync(m => m.EquipeId == equipeId && m.PerfilAluno.UsuarioId == usuarioId);
    }

    public async Task<bool> IsUsuarioInEquipeAsync(Guid equipeId, Guid usuarioId)
    {
        return await _dbSet.AnyAsync(m => m.EquipeId == equipeId && m.PerfilAluno.UsuarioId == usuarioId && m.IsActive && m.SaiuEm == null);
    }

    public async Task<bool> UsuarioJaEstaEmEquipeAsync(Guid usuarioId)
    {
        return await _dbSet.AnyAsync(m => m.PerfilAluno.UsuarioId == usuarioId && m.IsActive && m.SaiuEm == null);
    }

    public async Task<int> CountMembrosAtivosAsync(Guid equipeId)
    {
        return await _dbSet.CountAsync(m => m.EquipeId == equipeId && m.IsActive && m.SaiuEm == null);
    }

    public async Task<bool> RemoveFromEquipeAsync(Guid equipeId, Guid usuarioId)
    {
        var membro = await GetByEquipeAndUsuarioAsync(equipeId, usuarioId);
        if (membro == null)
            return false;

        membro.SaiuEm = DateTime.UtcNow;
        membro.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<MembroEquipe?> GetLiderEquipeAsync(Guid equipeId)
    {
        return await _dbSet
            .Include(m => m.PerfilAluno)
                .ThenInclude(pa => pa.Usuario)
            .Include(m => m.Equipe)
            .FirstOrDefaultAsync(m => m.EquipeId == equipeId && m.Cargo == TeamMemberRole.Lider && m.IsActive);
    }

    public async Task<IEnumerable<MembroEquipe>> GetMembrosComUsuarioAsync(Guid equipeId)
    {
        return await _dbSet
            .Include(m => m.PerfilAluno)
                .ThenInclude(pa => pa.Usuario)
            .Include(m => m.Equipe)
            .Where(m => m.EquipeId == equipeId && m.IsActive)
            .ToListAsync();
    }
}