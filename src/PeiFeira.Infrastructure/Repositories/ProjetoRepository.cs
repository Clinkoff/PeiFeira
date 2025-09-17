using PeiFeira.Domain.Entities.Projetos;
using PeiFeira.Domain.Interfaces;
using PeiFeira.Infrastructure.Data;

namespace PeiFeira.Infrastructure.Repositories;

public class ProjetoRepository : BaseRepository<Projeto> // IProjetoRepository
{
    public ProjetoRepository(PeiFeiraDbContext context) : base(context)
    {
    }

}