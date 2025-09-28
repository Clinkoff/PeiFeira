using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class MembroEquipeRepository : BaseRepository<MembroEquipe> // , IMembroEquipeRepository
{
    public MembroEquipeRepository(PeiFeiraDbContext context) : base(context)
    {
    }

}