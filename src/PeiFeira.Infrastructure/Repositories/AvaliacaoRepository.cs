using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Interfaces.Avaliacoes;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class AvaliacaoRepository : BaseRepository<Avaliacao>, IAvaliacaoRepository
{
    public AvaliacaoRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Avaliacao>> GetByEquipeIdAsync(Guid equipeId)
    {
        return await _dbSet
            .Include(a => a.Avaliador)
                .ThenInclude(av => av.Usuario)
            .Include(a => a.Equipe)
            .Where(a => a.EquipeId == equipeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Avaliacao>> GetByAvaliadorIdAsync(Guid avaliadorId)
    {
        return await _dbSet
            .Include(a => a.Equipe)
                .ThenInclude(e => e.Lider)
                    .ThenInclude(l => l.Usuario)
            .Include(a => a.Equipe)
                .ThenInclude(e => e.Projeto)
            .Where(a => a.AvaliadorId == avaliadorId)
            .ToListAsync();
    }

    public async Task<bool> JaAvaliadoAsync(Guid equipeId, Guid avaliadorId)
    {
        return await _dbSet.AnyAsync(a => a.EquipeId == equipeId && a.AvaliadorId == avaliadorId && a.IsActive);
    }

    public async Task<bool> PodeAvaliarAsync(Guid equipeId, Guid avaliadorId)
    {
        // Verifica se o avaliador não é o professor que acompanha o projeto (verificando através da DisciplinaPI)
        var equipe = await _context.Set<Domain.Entities.Equipes.Equipe>()
            .Include(e => e.Projeto)
                .ThenInclude(p => p!.DisciplinaPI)
            .FirstOrDefaultAsync(e => e.Id == equipeId);

        if (equipe?.Projeto?.DisciplinaPI == null)
            return true; // Se não há projeto ou disciplina, pode avaliar

        // Não pode avaliar se for o professor responsável pela disciplina do projeto
        return equipe.Projeto.DisciplinaPI.PerfilProfessorId != avaliadorId;
    }

    public async Task<decimal> GetNotaAvaliacaoEquipeAsync(Guid equipeId)
    {
        var avaliacoes = await _dbSet
            .Where(a => a.EquipeId == equipeId && a.IsActive && a.NotaFinal.HasValue)
            .ToListAsync();

        if (!avaliacoes.Any())
            return 0;

        return avaliacoes.Average(a => a.NotaFinal!.Value);
    }

    public async Task<decimal> GetMediaGeralAsync()
    {
        var avaliacoes = await _dbSet
            .Where(a => a.IsActive && a.NotaFinal.HasValue)
            .ToListAsync();

        if (!avaliacoes.Any())
            return 0;

        return avaliacoes.Average(a => a.NotaFinal!.Value);
    }

    public async Task<IEnumerable<Avaliacao>> GetAvaliacoesPorFaixaNotaAsync(decimal notaMin, decimal notaMax)
    {
        return await _dbSet
            .Include(a => a.Equipe)
            .Include(a => a.Avaliador)
                .ThenInclude(av => av.Usuario)
            .Where(a => a.NotaFinal.HasValue && a.NotaFinal >= notaMin && a.NotaFinal <= notaMax)
            .ToListAsync();
    }
}