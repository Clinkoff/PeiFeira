using Microsoft.EntityFrameworkCore;
using PeiFeira.Domain.Entities.Semestres;
using PeiFeira.Domain.Interfaces.Semestres;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class SemestreRepository : BaseRepository<Semestre>, ISemestre
{
    public SemestreRepository(PeiFeiraDbContext context) : base(context)
    {
    }

    public async Task<Semestre?> GetByAnoAndPeriodoAsync(int ano, int periodo)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.Ano == ano && s.Periodo == periodo);
    }

    public async Task<IEnumerable<Semestre>> GetByAnoAsync(int ano)
    {
        return await _dbSet
            .Where(s => s.Ano == ano)
            .OrderBy(s => s.Periodo)
            .ToListAsync();
    }

    public async Task<bool> ExistsByAnoAndPeriodoAsync(int ano, int periodo)
    {
        return await _dbSet
            .AnyAsync(s => s.Ano == ano && s.Periodo == periodo);
    }
}
