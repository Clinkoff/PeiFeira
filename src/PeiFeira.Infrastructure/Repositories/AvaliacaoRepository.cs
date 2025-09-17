using PeiFeira.Domain.Entities.Avaliacoes;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class AvaliacaoRepository : BaseRepository<Avaliacao> // IAvaliacaoRepository
{
    public AvaliacaoRepository(PeiFeiraDbContext context) : base(context)
    {
    }

}