using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class EquipeRepository : BaseRepository<Equipe> // , IEquipeRepository
{
    public EquipeRepository(PeiFeiraDbContext context) : base(context)
    {
    }


}