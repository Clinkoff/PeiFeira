using PeiFeira.Domain.Entities.Equipes;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class ConviteEquipeRepository : BaseRepository<ConviteEquipe> //, IConviteEquipeRepository
{
    public ConviteEquipeRepository(PeiFeiraDbContext context) : base(context)
    {
    }

}