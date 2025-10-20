using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Enums;
using PeiFeira.Domain.Interfaces.Equipes;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class ConviteEquipeRepository : BaseRepository<ConviteEquipe>, IConviteEquipeRepository
{
    public ConviteEquipeRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ConviteEquipe>> GetByEquipeIdAsync(Guid equipeId)
    {
        return await _dbSet
            .Include(c => c.Equipe)
            .Include(c => c.ConvidadoPor)
                .ThenInclude(cp => cp.Usuario)
            .Include(c => c.Convidado)
                .ThenInclude(c => c.Usuario)
            .Where(c => c.EquipeId == equipeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ConviteEquipe>> GetByConvidadoIdAsync(Guid convidadoId)
    {
        return await _dbSet
            .Include(c => c.Equipe)
            .Include(c => c.ConvidadoPor)
                .ThenInclude(cp => cp.Usuario)
            .Include(c => c.Convidado)
                .ThenInclude(c => c.Usuario)
            .Where(c => c.ConvidadoPerfilAlunoId == convidadoId)
            .ToListAsync();
    }

    public async Task<ConviteEquipe?> GetByEquipeAndUsuarioAsync(Guid equipeId, Guid usuarioId)
    {
        return await _dbSet
            .Include(c => c.Equipe)
            .Include(c => c.ConvidadoPor)
                .ThenInclude(cp => cp.Usuario)
            .Include(c => c.Convidado)
                .ThenInclude(c => c.Usuario)
            .FirstOrDefaultAsync(c => c.EquipeId == equipeId && c.Convidado.UsuarioId == usuarioId);
    }

    public async Task<IEnumerable<ConviteEquipe>> GetPendentesAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(c => c.Equipe)
            .Include(c => c.ConvidadoPor)
                .ThenInclude(cp => cp.Usuario)
            .Include(c => c.Convidado)
                .ThenInclude(c => c.Usuario)
            .Where(c => c.Convidado.UsuarioId == usuarioId && c.Status == StatusConvite.Pendente)
            .ToListAsync();
    }

    public async Task<IEnumerable<ConviteEquipe>> GetByStatusAsync(StatusConvite status)
    {
        return await _dbSet
            .Include(c => c.Equipe)
            .Include(c => c.ConvidadoPor)
                .ThenInclude(cp => cp.Usuario)
            .Include(c => c.Convidado)
                .ThenInclude(c => c.Usuario)
            .Where(c => c.Status == status)
            .ToListAsync();
    }

    public async Task<bool> JaFoiConvidadoAsync(Guid equipeId, Guid usuarioId)
    {
        return await _dbSet.AnyAsync(c => c.EquipeId == equipeId && c.Convidado.UsuarioId == usuarioId);
    }

    public async Task<bool> TemConvitePendenteAsync(Guid equipeId, Guid usuarioId)
    {
        return await _dbSet.AnyAsync(c => c.EquipeId == equipeId && c.Convidado.UsuarioId == usuarioId && c.Status == StatusConvite.Pendente);
    }

    public async Task<bool> CancelarConvitesEquipeAsync(Guid equipeId)
    {
        var convitesPendentes = await _dbSet
            .Where(c => c.EquipeId == equipeId && c.Status == StatusConvite.Pendente)
            .ToListAsync();

        foreach (var convite in convitesPendentes)
        {
            convite.Status = StatusConvite.Rejeitado;
            convite.MotivoResposta = "Convite cancelado automaticamente";
            convite.RespondidoEm = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return convitesPendentes.Any();
    }

    public async Task<int> CountConvitesPendentesAsync(Guid usuarioId)
    {
        return await _dbSet.CountAsync(c => c.Convidado.UsuarioId == usuarioId && c.Status == StatusConvite.Pendente);
    }
}